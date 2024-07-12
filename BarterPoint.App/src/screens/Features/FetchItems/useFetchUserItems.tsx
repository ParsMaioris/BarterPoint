import { useState, useEffect } from 'react';
import { API, Auth, Storage, graphqlOperation } from 'aws-amplify';
import { itemsByUserID } from '../../../graphql/queries';
import { Item, ItemsByUserIDQuery } from '../../../API';

const useFetchUserItems = () => {
  const [userItems, setUserItems] = useState<Item[]>([]);

  const fetchItems = async () => {
    try {
      const currentUser = await Auth.currentAuthenticatedUser();
      const userId = currentUser.attributes.sub;

      const itemsData = (await API.graphql(
        graphqlOperation(itemsByUserID, { userID: userId })
      )) as { data: ItemsByUserIDQuery };
      const items = itemsData.data.itemsByUserID?.items ?? [];

      const activeItems = items.filter(
        (item): item is Item => item !== null && item._deleted !== true
      );

      const itemsWithImageUrls = await Promise.all(
        activeItems.map(async (item) => {
          const imageUrls = await Promise.all(
            (item.images || [])
              .filter((imageKey): imageKey is string => imageKey !== null)
              .map(async (imageKey) => {
                const url = await Storage.get(imageKey, { level: "public" });
                return url;
              })
          );
          return {
            ...item,
            images: imageUrls.filter((url): url is string => url !== null),
          };
        })
      );

      setUserItems(itemsWithImageUrls as Item[]);
    } catch (error) {
      console.error("Error fetching user items:", error);
    }
  };

  useEffect(() => {
    fetchItems();
  }, []);

  const clearUserItems = () => setUserItems([]);

  return [userItems, fetchItems, clearUserItems] as const;
};

export default useFetchUserItems;