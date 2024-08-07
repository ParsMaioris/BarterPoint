import React, {useState} from "react"
import {View, Text, StyleSheet, TouchableOpacity, Modal} from "react-native"
import {Ionicons} from '@expo/vector-icons'
import {buttonStyles} from "../styles/common/ButtonStyles"
import {useNavigation} from "@react-navigation/native"
import {StackNavigationProp} from "@react-navigation/stack"
import {RootStackParamList} from "../navigation/navigationTypes"
import ProductListScreen from "./ProductListScreen"
import MyProductsScreen from "./MyProductsScreen"
import FavoritesScreen from "./FavoritesScreen"

const ProductsScreen: React.FC = () =>
{
    const [isMyProductsScreenModalVisible, setMyProductsScreenModalVisible] = useState(false)
    const [isFavoritesScreenModalVisible, setFavoritesScreenModalVisible] = useState(false)
    const navigation = useNavigation<StackNavigationProp<RootStackParamList>>()

    return (
        <View style={styles.container}>
            <View style={styles.section}>
                <ProductListScreen showCategoryFilter={true} />
            </View>
            <View style={styles.buttonContainer}>
                <TouchableOpacity
                    style={[buttonStyles.button, styles.buttonSpacing]}
                    onPress={() => setMyProductsScreenModalVisible(true)}
                >
                    <Text style={[buttonStyles.buttonText]}>My Products</Text>
                </TouchableOpacity>
                <TouchableOpacity
                    style={[buttonStyles.button, styles.buttonSpacing]}
                    onPress={() => setFavoritesScreenModalVisible(true)}
                >
                    <Text style={[buttonStyles.buttonText]}>My Favorites</Text>
                </TouchableOpacity>
            </View>

            <Modal
                visible={isMyProductsScreenModalVisible}
                animationType="slide"
                onRequestClose={() => setMyProductsScreenModalVisible(false)}
            >
                <View style={styles.modalContainer}>
                    <View style={styles.sectionHeader}>
                        <Text style={styles.sectionTitle}>My Products</Text>
                        <TouchableOpacity onPress={() => navigation.navigate("PostItemScreen")}>
                            <Ionicons name="add-circle-outline" size={24} color="#8B4513" />
                        </TouchableOpacity>
                    </View>
                    <MyProductsScreen />
                    <TouchableOpacity
                        style={[buttonStyles.button]}
                        onPress={() => setMyProductsScreenModalVisible(false)}
                    >
                        <Text style={[buttonStyles.buttonText]}>Close</Text>
                    </TouchableOpacity>
                </View>
            </Modal>

            <Modal
                visible={isFavoritesScreenModalVisible}
                animationType="slide"
                onRequestClose={() => setFavoritesScreenModalVisible(false)}
            >
                <View style={styles.modalContainer}>
                    <View style={styles.sectionHeader}>
                        <Text style={styles.sectionTitle}>My Favorites</Text>
                    </View>
                    <FavoritesScreen />
                    <TouchableOpacity
                        style={[buttonStyles.button]}
                        onPress={() => setFavoritesScreenModalVisible(false)}
                    >
                        <Text style={[buttonStyles.buttonText]}>Close</Text>
                    </TouchableOpacity>
                </View>
            </Modal>
        </View>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 15,
        backgroundColor: "#F5F5F5",
    },
    section: {
        flex: 1,
        marginBottom: 20,
        padding: 20,
        backgroundColor: "#FFFFFF",
        borderRadius: 10,
        shadowColor: "#000",
        shadowOffset: {
            width: 0,
            height: 2,
        },
        shadowOpacity: 0.1,
        shadowRadius: 4,
        elevation: 5,
    },
    buttonContainer: {
        flexDirection: "row",
        justifyContent: "space-between",
        marginBottom: 20,
    },
    buttonSpacing: {
        flex: 1,
        marginHorizontal: 5,
    },
    sectionHeader: {
        flexDirection: "row",
        alignItems: "center",
        justifyContent: "space-between",
        marginBottom: 15,
    },
    sectionTitle: {
        fontSize: 20,
        fontWeight: "600",
        color: "#333333",
    },
    modalContainer: {
        flex: 1,
        padding: 20,
        backgroundColor: "#FFFFFF",
    },
})

export default ProductsScreen