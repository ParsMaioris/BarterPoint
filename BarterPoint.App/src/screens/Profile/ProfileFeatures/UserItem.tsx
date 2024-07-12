import React from "react";
import {
  View,
  Text,
  Image,
  StyleSheet,
  TouchableOpacity,
  Alert,
} from "react-native";
import { Item } from "../../../API";

type UserItemProps = {
  item: Item;
  onDelete: (itemId: string) => void;
  onEdit: (itemId: string) => void;
  onImageSelect: (item: Item) => void;
};

const UserItem: React.FC<UserItemProps> = ({
  item,
  onDelete,
  onEdit,
  onImageSelect,
}) => {
  if (item._deleted === true) {
    // Skip rendering deleted items
    return null;
  }

  const confirmDelete = () => {
    Alert.alert(
      "Confirm Delete",
      "Are you sure you want to delete this item?",
      [
        {
          text: "Cancel",
          style: "cancel",
        },
        {
          text: "Yes",
          onPress: () => onDelete(item.id), // Call the onDelete function with the item's ID
        },
      ]
    );
  };

  const confirmEdit = () => {
    onEdit(item.id);
  };

  return (
    <View style={styles.itemContainer}>
      <View style={styles.imageContainer}>
        <Text style={styles.itemTitle}>{item.title}</Text>
        {item.images && item.images?.length > 0 && item.images[0] && (
          <TouchableOpacity onPress={() => onImageSelect(item)}>
            <Image source={{ uri: item.images?.[0] }} style={styles.image} />
          </TouchableOpacity>
        )}
      </View>
      <View style={styles.descriptionContainer}>
        <Text style={styles.itemDescription}>{item.description}</Text>
      </View>
      <View style={styles.actionButtonsContainer}>
        <TouchableOpacity onPress={confirmEdit} style={styles.editButton}>
          <Text>Edit</Text>
        </TouchableOpacity>
        <TouchableOpacity onPress={confirmDelete} style={styles.deleteButton}>
          <Text>Delete</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  itemContainer: {
    flexDirection: "row", // align children in a row
    justifyContent: "space-between", // space out children
    alignItems: "center", // center children vertically
    width: "100%", // take up full width
    padding: 10, // add some padding
  },
  imageContainer: {
    width: "30%", // adjust the width as needed
  },
  image: {
    width: "100%",
    height: 100, // adjust height as needed
    resizeMode: "cover",
  },
  descriptionContainer: {
    width: "40%", // adjust the width as needed
    justifyContent: "center",
    alignItems: "flex-start", // align text to the start
  },
  itemTitle: {
    fontSize: 16,
    fontWeight: "bold",
  },
  itemDescription: {
    fontSize: 14,
    padding: 10,
  },
  actionButtonsContainer: {
    width: "30%", // adjust the width as needed
    justifyContent: "center",
    alignItems: "flex-end", // align buttons to the end
  },
  button: {
    // style for your buttons
  },
  deleteButton: {
    backgroundColor: "red",
    width: "75%",
    padding: 10,
    borderRadius: 5,
  },
  editButton: {
    backgroundColor: "lightblue",
    width: "75%",
    padding: 10,
    borderRadius: 5,
  },
});

export default UserItem;
