import React, {useState} from 'react'
import
{
    View,
    Text,
    FlatList,
    StyleSheet,
    Image,
    TouchableOpacity,
    Modal,
    ScrollView,
    Alert,
} from 'react-native'
import {useSelector, useDispatch} from 'react-redux'
import {MaterialIcons} from '@expo/vector-icons'
import {RootState, AppDispatch} from '../redux/Store'
import {buttonStyles} from '../styles/common/ButtonStyles'
import {selectAllProducts} from '../redux/Selectors'
import {removeBid, fetchAllBids} from '../api/ApiService'
import {Product} from '../models/Product'
import CategoryFilter from '../components/common/CategoryFilter'
import {ProductCategory} from '../models/ProductCategory'
import {Bid} from '../models/Bid'

const BidScreen: React.FC = () =>
{
    const bids = useSelector((state: RootState) => state.bids.bids) as Bid[]
    const userProducts = useSelector((state: RootState) => state.products.userProducts) as Product[]
    const allProducts = useSelector(selectAllProducts)
    const [selectedProduct, setSelectedProduct] = useState<Product | null>(null)
    const [isModalVisible, setModalVisible] = useState(false)
    const [selectedCategory, setSelectedCategory] = useState<ProductCategory | null>(null)
    const dispatch = useDispatch<AppDispatch>()

    const findProductById = (id: string): Product | undefined =>
        allProducts.find((product) => product.id === id)

    const handleProductPress = (product: Product) =>
    {
        setSelectedProduct(product)
        setModalVisible(true)
    }

    const handleRemoveBid = async (bidId: number) =>
    {
        const resultAction = await dispatch(removeBid(bidId))

        if (removeBid.fulfilled.match(resultAction))
        {
            await dispatch(fetchAllBids())
            Alert.alert('Success', 'Bid removed successfully.')
        } else
        {
            const errorMessage = (resultAction.payload as string) || 'Failed to remove bid.'
            Alert.alert('Error', errorMessage)
        }
    }

    const renderItem = ({item}: {item: Bid}) =>
    {
        const product1 = findProductById(item.product1Id)
        const product2 = findProductById(item.product2Id)

        if (!product1 || !product2) return null

        const shouldDisplayProduct1 = selectedCategory === null || product1.category === selectedCategory
        const shouldDisplayProduct2 = selectedCategory === null || product2.category === selectedCategory

        if (!shouldDisplayProduct1 && !shouldDisplayProduct2) return null

        return (
            <View style={styles.bidItem}>
                <TouchableOpacity style={styles.productContainer} onPress={() => handleProductPress(product1)}>
                    <Image source={{uri: product1.image}} style={styles.productImage} />
                    <View style={styles.ProductDetailScreens}>
                        <Text style={styles.productName}>{product1.name}</Text>
                    </View>
                </TouchableOpacity>
                <MaterialIcons name="swap-horiz" size={24} color="black" style={styles.bidIcon} />
                <TouchableOpacity style={styles.productContainer} onPress={() => handleProductPress(product2)}>
                    <Image source={{uri: product2.image}} style={styles.productImage} />
                    <View style={styles.ProductDetailScreens}>
                        <Text style={styles.productName}>{product2.name}</Text>
                    </View>
                </TouchableOpacity>
                <TouchableOpacity onPress={() => handleRemoveBid(item.id)}>
                    <MaterialIcons name="delete" size={24} color="#696969" style={styles.deleteIcon} />
                </TouchableOpacity>
            </View>
        )
    }

    return (
        <View style={styles.container}>
            <CategoryFilter selectedCategory={selectedCategory} onSelectCategory={setSelectedCategory} />
            <FlatList
                data={bids}
                renderItem={renderItem}
                keyExtractor={(item) => item.id.toString()}
            />
            {selectedProduct && (
                <Modal
                    visible={isModalVisible}
                    transparent={true}
                    animationType="slide"
                    onRequestClose={() => setModalVisible(false)}
                >
                    <View style={styles.modalContainer}>
                        <View style={styles.modalContent}>
                            <Image source={{uri: selectedProduct.image}} style={styles.modalImage} />
                            <Text style={styles.modalTitle}>{selectedProduct.name}</Text>
                            <ScrollView>
                                <Text style={styles.modalDescription}>{selectedProduct.description}</Text>
                                <Text style={styles.modalTradeFor}>Trade for: {selectedProduct.tradeFor}</Text>
                            </ScrollView>
                            <TouchableOpacity
                                style={[buttonStyles.button, buttonStyles.redButton, {marginTop: 20}]}
                                onPress={() => setModalVisible(false)}
                            >
                                <Text style={[buttonStyles.buttonText, buttonStyles.redButtonText]}>Close</Text>
                            </TouchableOpacity>
                        </View>
                    </View>
                </Modal>
            )}
        </View>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 16,
        backgroundColor: '#fff',
    },
    title: {
        fontSize: 24,
        fontWeight: 'bold',
        marginBottom: 16,
    },
    bidItem: {
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        marginBottom: 16,
        padding: 8,
        backgroundColor: '#f9f9f9',
        borderRadius: 8,
    },
    productContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        flex: 1,
    },
    productImage: {
        width: 50,
        height: 50,
        borderRadius: 8,
        marginRight: 16,
    },
    ProductDetailScreens: {
        flex: 1,
    },
    productName: {
        fontSize: 16,
        fontWeight: 'bold',
    },
    bidIcon: {
        marginHorizontal: 8,
    },
    deleteIcon: {
        marginHorizontal: 8,
    },
    modalContainer: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: 'rgba(0,0,0,0.5)',
    },
    modalContent: {
        width: '80%',
        backgroundColor: '#fff',
        padding: 20,
        borderRadius: 10,
        alignItems: 'center',
    },
    modalImage: {
        width: 100,
        height: 100,
        borderRadius: 8,
        marginBottom: 16,
    },
    modalTitle: {
        fontSize: 20,
        fontWeight: 'bold',
        marginBottom: 8,
    },
    modalDescription: {
        fontSize: 16,
        marginBottom: 8,
    },
    modalTradeFor: {
        fontSize: 16,
        fontWeight: 'bold',
    },
})

export default BidScreen