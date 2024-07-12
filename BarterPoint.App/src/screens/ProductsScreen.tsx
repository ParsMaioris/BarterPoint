import React, {useState} from "react"
import {View, Text, StyleSheet, TouchableOpacity, Modal} from "react-native"
import {Ionicons} from '@expo/vector-icons'
import {buttonStyles} from "./ButtonStyles"
import ProductListScreen from "./ProductListScreen"
import MyProducts from "./MyProducts"
import {useNavigation} from "@react-navigation/native"
import {StackNavigationProp} from "@react-navigation/stack"
import {RootStackParamList} from "../navigation/navigationTypes"

const ProductsScreen: React.FC = () =>
{
    const [isMyProductsModalVisible, setMyProductsModalVisible] = useState(false)
    const navigation = useNavigation<StackNavigationProp<RootStackParamList>>()

    return (
        <View style={styles.container}>
            <View style={styles.section}>
                <ProductListScreen showCategoryFilter={true} />
            </View>
            <TouchableOpacity
                style={[buttonStyles.button]}
                onPress={() => setMyProductsModalVisible(true)}
            >
                <Text style={[buttonStyles.buttonText]}>My Products</Text>
            </TouchableOpacity>

            <Modal
                visible={isMyProductsModalVisible}
                animationType="slide"
                onRequestClose={() => setMyProductsModalVisible(false)}
            >
                <View style={styles.modalContainer}>
                    <View style={styles.sectionHeader}>
                        <Text style={styles.sectionTitle}>My Products</Text>
                        <TouchableOpacity onPress={() => navigation.navigate("PostItem")}>
                            <Ionicons name="add-circle-outline" size={24} color="#8B4513" />
                        </TouchableOpacity>
                    </View>
                    <MyProducts />
                    <TouchableOpacity
                        style={[buttonStyles.button]}
                        onPress={() => setMyProductsModalVisible(false)}
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
        justifyContent: "center",
        backgroundColor: "#FFFFFF",
    },
})

export default ProductsScreen