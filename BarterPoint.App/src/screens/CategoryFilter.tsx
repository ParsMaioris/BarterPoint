import React, {useState} from "react"
import
{
    View,
    Text,
    TouchableOpacity,
    Modal,
    ScrollView,
    StyleSheet,
} from "react-native"
import {ProductCategory} from "../models/ProductCategory"
import {buttonStyles} from "./ButtonStyles"
import {Ionicons} from '@expo/vector-icons'

interface CategoryFilterProps
{
    selectedCategory: ProductCategory | null
    onSelectCategory: (category: ProductCategory | null) => void
}

const formatCategoryName = (category: string): string =>
{
    return category.replace(/([A-Z])/g, ' $1').trim()
}

const CategoryFilter: React.FC<CategoryFilterProps> = ({
    selectedCategory,
    onSelectCategory,
}) =>
{
    const [modalVisible, setModalVisible] = useState(false)

    const renderCategoryOption = (category: ProductCategory) => (
        <TouchableOpacity
            key={category}
            onPress={() =>
            {
                onSelectCategory(category)
                setModalVisible(false)
            }}
            style={styles.option}
        >
            <Text style={styles.optionText}>{formatCategoryName(category)}</Text>
        </TouchableOpacity>
    )

    return (
        <View>
            <TouchableOpacity
                onPress={() => setModalVisible(true)}
                style={[buttonStyles.dropdownButton, styles.dropdownButton]}
            >
                <Text style={[buttonStyles.dropdownButtonText, styles.dropdownButtonText]}>
                    {selectedCategory === null ? "All Categories" : formatCategoryName(selectedCategory)}
                </Text>
                <Ionicons name="chevron-down" size={24} style={styles.icon} />
            </TouchableOpacity>

            <Modal
                transparent={true}
                visible={modalVisible}
                onRequestClose={() => setModalVisible(false)}
            >
                <View style={styles.modalOverlay}>
                    <View style={styles.modalContent}>
                        <ScrollView>
                            <TouchableOpacity
                                onPress={() =>
                                {
                                    onSelectCategory(null)
                                    setModalVisible(false)
                                }}
                                style={styles.option}
                            >
                                <Text style={styles.optionText}>All Categories</Text>
                            </TouchableOpacity>
                            {Object.values(ProductCategory).map(value => renderCategoryOption(value as ProductCategory))}
                        </ScrollView>
                    </View>
                </View>
            </Modal>
        </View>
    )
}

const styles = StyleSheet.create({
    dropdownButton: {
        marginBottom: 10,
        flexDirection: "row",
        alignItems: "center",
        justifyContent: "center",
    },
    dropdownButtonText: {
        fontSize: 18,
        fontWeight: "bold",
        marginRight: 10,
    },
    icon: {
        alignSelf: "center",
    },
    modalOverlay: {
        flex: 1,
        justifyContent: "center",
        alignItems: "center",
        backgroundColor: "rgba(0,0,0,0.5)",
    },
    modalContent: {
        width: "80%",
        maxHeight: "80%",
        backgroundColor: "#fff",
        borderRadius: 10,
        padding: 20,
        shadowColor: "#000",
        shadowOffset: {width: 0, height: 2},
        shadowOpacity: 0.25,
        shadowRadius: 3.84,
        elevation: 5,
    },
    option: {
        paddingVertical: 12,
        paddingHorizontal: 10,
        borderBottomWidth: 1,
        borderBottomColor: "#eee",
    },
    optionText: {
        fontSize: 16,
        color: "#333",
    },
})

export default CategoryFilter