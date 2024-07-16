import React, {useState} from 'react'
import
{
    View,
    Text,
    FlatList,
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
import {removeBid, fetchAllBids, approveBid, fetchProductsNotOwnedByUser, fetchProductsByOwner} from '../api/ApiService'
import {Product} from '../models/Product'
import CategoryFilter from '../components/common/CategoryFilter'
import {ProductCategory} from '../models/ProductCategory'
import {Bid} from '../models/Bid'
import {BidScreenStyles} from '../styles/specific/BidScreenStyles'

const BidScreen: React.FC = () =>
{
    const bids = useSelector((state: RootState) => state.bids.bids) as Bid[]
    const allProducts = useSelector(selectAllProducts)
    const [selectedProduct, setSelectedProduct] = useState<Product | null>(null)
    const [isModalVisible, setModalVisible] = useState(false)
    const [selectedCategory, setSelectedCategory] = useState<ProductCategory | null>(null)
    const dispatch = useDispatch<AppDispatch>()
    const userId = useSelector((state: RootState) => state.users.userId) as string

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

    const handleApproveBid = async (bidId: number, userId: string) =>
    {
        const resultAction = await dispatch(approveBid(bidId))

        if (approveBid.fulfilled.match(resultAction))
        {
            await dispatch(fetchAllBids())
            await dispatch(fetchProductsNotOwnedByUser(userId))
            await dispatch(fetchProductsByOwner(userId))
            Alert.alert('Success', 'Bid approved successfully.')
        } else
        {
            const errorMessage = (resultAction.payload as string) || 'Failed to approve bid.'
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
            <View style={BidScreenStyles.bidCard}>
                <View style={BidScreenStyles.productRow}>
                    <TouchableOpacity style={BidScreenStyles.productContainer} onPress={() => handleProductPress(product1)}>
                        <Image source={{uri: product1.image}} style={BidScreenStyles.productImage} />
                        <View style={BidScreenStyles.productDetailScreens}>
                            <Text style={BidScreenStyles.productName}>{product1.name}</Text>
                        </View>
                    </TouchableOpacity>
                    <MaterialIcons name="swap-horiz" size={24} color="black" style={BidScreenStyles.bidIcon} />
                    <TouchableOpacity style={BidScreenStyles.productContainer} onPress={() => handleProductPress(product2)}>
                        <Image source={{uri: product2.image}} style={BidScreenStyles.productImage} />
                        <View style={BidScreenStyles.productDetailScreens}>
                            <Text style={BidScreenStyles.productName}>{product2.name}</Text>
                        </View>
                    </TouchableOpacity>
                </View>
                <View style={BidScreenStyles.actionRow}>
                    <TouchableOpacity onPress={() => handleApproveBid(item.id, userId)} style={BidScreenStyles.actionButton}>
                        <MaterialIcons name="check-circle" size={24} color="#4CAF50" />
                        <Text style={BidScreenStyles.actionText}>Approve</Text>
                    </TouchableOpacity>
                    <TouchableOpacity onPress={() => handleRemoveBid(item.id)} style={BidScreenStyles.actionButton}>
                        <MaterialIcons name="delete" size={24} color="#F44336" />
                        <Text style={BidScreenStyles.actionText}>Remove</Text>
                    </TouchableOpacity>
                </View>
            </View>
        )
    }

    return (
        <View style={BidScreenStyles.container}>
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
                    <View style={BidScreenStyles.modalContainer}>
                        <View style={BidScreenStyles.modalContent}>
                            <Image source={{uri: selectedProduct.image}} style={BidScreenStyles.modalImage} />
                            <Text style={BidScreenStyles.modalTitle}>{selectedProduct.name}</Text>
                            <ScrollView>
                                <Text style={BidScreenStyles.modalDescription}>{selectedProduct.description}</Text>
                                <Text style={BidScreenStyles.modalTradeFor}>Trade for: {selectedProduct.tradeFor}</Text>
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

export default BidScreen