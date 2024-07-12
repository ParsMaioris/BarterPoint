import { useEffect, useState } from "react";
import { Alert } from "react-native";
import * as ImagePicker from "expo-image-picker";
import UploadImage from "../Features/PostItem/UploadItem/uploadImage";
import { API, Auth, Storage, graphqlOperation } from "aws-amplify";
import {
  ProfileScreenNavigationProp,
  Offer,
  RouteParams,
} from "models/navigationTypes";
import CheckAuthStatus from "./../../utils/CheckAuthStatus/CheckAuthStatus";
import { Item } from "../../API";
import { deleteItem, updateItem } from "../../graphql/mutations";
import useFetchUserItems from "../../screens/Features/FetchItems/useFetchUserItems";

const useProfile = (
  navigation: ProfileScreenNavigationProp,
  route: { params?: RouteParams }
) => {
  const [imageUri, setImageUri] = useState<string | null>(null);
  const [userItems, refreshUserItems, clearUserItems] = useFetchUserItems();
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [selectedImages, setSelectedImages] = useState<string[]>([]);
  const [offers, setOffers] = useState<Offer[]>([
    { productOffered: "Electric scooter", productRequested: "Bicycle" },
    { productOffered: "Keyboard piano", productRequested: "Guitar" },
  ]);

  useEffect(() => {
    const verifyAuthStatus = async () => {
      const isAuthenticated = await CheckAuthStatus();
      if (!isAuthenticated) {
        navigation.navigate("SignIn");
      } else {
        fetchUserProfile();
      }
    };

    verifyAuthStatus();
  }, [navigation]);

  useEffect(() => {
    if (route.params?.newOffer) {
      setOffers((prevOffers) => [...prevOffers, route.params.newOffer]);
    }
  }, [route.params?.newOffer]);

  const fetchUserProfile = async () => {
    try {
      const currentUser = await Auth.currentAuthenticatedUser();
      const userId = currentUser.attributes.sub;

      const s3Path = `users/${userId}/profile/`;
      const files = await Storage.list(s3Path, {
        level: "public",
        pageSize: 1,
      });

      if (files.results.length > 0) {
        const firstFile = files.results[0];
        const imageUrl = await Storage.get(firstFile.key as string, {
          level: "public",
        });
        setImageUri(imageUrl);
      } else {
        console.log("No profile image found for the user.");
      }
    } catch (error) {
      console.error("Error fetching user profile:", error);
    }
  };

  const handleLogout = async () => {
    try {
      await Auth.signOut();
      clearUserItems();
      setImageUri(null);
      navigation.navigate("SignIn");
    } catch (error) {
      console.error("Error signing out: ", error);
      Alert.alert("Logout Failed", "Unable to logout at this time.");
    }
  };

  const handleProfileImage = async () => {
    const permissionResult =
      await ImagePicker.requestMediaLibraryPermissionsAsync();

    if (permissionResult.granted === false) {
      Alert.alert(
        "Permission required",
        "Sorry, we need camera roll permissions to make this work!"
      );
      return;
    }

    let result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true,
      aspect: [1, 1],
      quality: 1,
    });

    if (!result.canceled && result.assets && result.assets.length > 0) {
      const currentUser = await Auth.currentAuthenticatedUser();
      const userId = currentUser.attributes.sub;
      const s3Path = `users/${userId}/profile/`;
      const files = await Storage.list(s3Path, {
        level: "public",
        pageSize: 1,
      });
      if (files.results.length > 0) {
        await Storage.remove(files.results[0].key as string, {
          level: "public",
        });
      }
      const imageUri = result.assets[0].uri;
      await UploadImage({ imageUri: imageUri, imageType: "profile" });
      setImageUri(imageUri);
    } else {
      console.log("Image picking was cancelled or no image was selected");
    }
  };

  const handleEditItem = async (item: Item) => {
    try {
      if (!item.id) {
        console.error("Invalid updated item ID");
        return;
      }

      await API.graphql(
        graphqlOperation(updateItem, {
          input: {
            id: item.id,
          },
        })
      );
      refreshUserItems();
    } catch (error) {
      console.error("Error updating item:", error);
      Alert.alert("Update Failed", "Unable to update the item at this time.");
    }
  };

  const handleDeleteItem = async (item: Item) => {
    try {
      if (!item.id || !item.images) {
        console.error("Invalid item data for deletion");
        return;
      }

      for (const imageUrl of item.images) {
        const urlParts = new URL(decodeURIComponent(imageUrl as string));
        const key = urlParts.pathname.substring(
          urlParts.pathname.indexOf("public/") + 7
        );
        try {
          await Storage.remove(key, { level: "public" });
        } catch (error) {
          console.error(`Error deleting image with key ${key}:`, error);
        }
      }

      await API.graphql(
        graphqlOperation(deleteItem, {
          input: {
            id: item.id,
            _version: item._version,
          },
        })
      );
      refreshUserItems();
    } catch (error) {
      console.error("Error deleting item:", error);
      Alert.alert("Delete Failed", "Unable to delete the item at this time.");
    }
  };

  return {
    imageUri,
    userItems,
    isModalVisible,
    selectedImages,
    offers,
    setIsModalVisible,
    setSelectedImages,
    handleLogout,
    handleProfileImage,
    handleEditItem,
    handleDeleteItem,
  };
};

export default useProfile;
