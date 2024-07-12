import { Item } from "API";
import React from "react";
import {
  View,
  Text,
  Image,
  StyleSheet,
  TouchableOpacity,
  ActivityIndicator,
} from "react-native";

const ItemCard: React.FC<{ item: Item; navigation: any }> = ({
  item,
  navigation,
}) => {
  const defaultImageUrl = "";

  const handlePress = () => {
    navigation.navigate("ItemDetails", { item });
  };

  return (
    <View style={styles.card}>
      {item.images && item.images[0] ? (
        <TouchableOpacity onPress={handlePress}>
          <Image source={{ uri: item.images[0] }} style={styles.image} />
        </TouchableOpacity>
      ) : (
        <TouchableOpacity onPress={handlePress}>
          <Image source={{ uri: defaultImageUrl }} style={styles.image} />
        </TouchableOpacity>
      )}
    </View>
  );
};

const styles = StyleSheet.create({
  card: {},
  image: {
    width: 125, // example width
    height: 125, // example height
    resizeMode: "cover", // or 'cover'
  },
});

export default ItemCard;
