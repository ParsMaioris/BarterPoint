import React from "react"
import {View, Text, TouchableOpacity, StyleSheet} from "react-native"
import {useNavigation} from "@react-navigation/native"

import {LandingPageScreenScreenNavigationProp} from "../navigation/navigationTypes"
import {buttonStyles} from "../styles/common/ButtonStyles"

const LandingPageButtons: React.FC = () =>
{
  const navigation = useNavigation<LandingPageScreenScreenNavigationProp>()

  const goToProfile = () => navigation.navigate("Profile")
  const goToBids = () => navigation.navigate("BidScreen")
  const goToProductListScreen = () => navigation.navigate("ProductsScreen")

  return (
    <View style={styles.buttonContainer}>
      <TouchableOpacity style={buttonStyles.button} onPress={goToProfile}>
        <Text style={buttonStyles.buttonText}>Profile</Text>
      </TouchableOpacity>
      <TouchableOpacity style={buttonStyles.button} onPress={goToProductListScreen}>
        <Text style={buttonStyles.buttonText}>Products</Text>
      </TouchableOpacity>
      <TouchableOpacity style={buttonStyles.button} onPress={goToBids}>
        <Text style={buttonStyles.buttonText}>Bids</Text>
      </TouchableOpacity>
    </View>
  )
}

const styles = StyleSheet.create({
  buttonContainer: {
    flexDirection: "row",
    justifyContent: "space-around",
    marginTop: 20,
  },
})

export default LandingPageButtons
