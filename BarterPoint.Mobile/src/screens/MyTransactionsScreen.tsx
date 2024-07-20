import React, {useEffect, useState} from 'react'
import {View, Text, FlatList, StyleSheet, Image, TouchableOpacity, Modal} from 'react-native'
import {useSelector, useDispatch} from 'react-redux'
import {RootState, AppDispatch} from '../redux/Store'
import {getUserTransactions} from '../api/ApiService'
import {getUserRating, addRating} from '../api/ApiService'
import {TransactionHistory} from '../models/TransactionHistory'
import {AddRatingRequest} from '../api/models/AddRatingRequest'
import {FontAwesome} from '@expo/vector-icons'
import {buttonStyles} from '../styles/common/ButtonStyles'

const MyTransactionsScreen: React.FC = () =>
{
    const dispatch = useDispatch<AppDispatch>()
    const userId = useSelector((state: RootState) => state.users.userId) as string
    const userTransactions = useSelector((state: RootState) => state.transactions.user)
    const loading = useSelector((state: RootState) => state.transactions.loading)
    const error = useSelector((state: RootState) => state.transactions.error)

    const [modalVisible, setModalVisible] = useState(false)
    const [currentCounterparty, setCurrentCounterparty] = useState('')
    const [currentTransaction, setCurrentTransaction] = useState<TransactionHistory | null>(null)
    const [rating, setRating] = useState(0)
    const [counterpartyRatings, setCounterpartyRatings] = useState<{[key: string]: number | null}>({})

    useEffect(() =>
    {
        if (userId)
        {
            dispatch(getUserTransactions(userId))
        }
    }, [dispatch, userId])

    useEffect(() =>
    {
        const fetchRatings = async () =>
        {
            const ratings: {[key: string]: number | null} = {}

            for (const transaction of userTransactions)
            {
                const counterpartyId = transaction.sellerId === userId ? transaction.buyerId : transaction.sellerId
                if (!ratings[counterpartyId])
                {
                    try
                    {
                        const ratingData = await getUserRating(counterpartyId)
                        ratings[counterpartyId] = ratingData.averageRating || null
                    } catch (error)
                    {
                        ratings[counterpartyId] = null
                    }
                }
            }

            setCounterpartyRatings(ratings)
        }

        fetchRatings()
    }, [userTransactions, userId])

    const handleRateCounterparty = async () =>
    {
        if (currentTransaction)
        {
            const ratingRequest: AddRatingRequest = {
                raterId: userId,
                rateeId: currentCounterparty,
                rating: rating,
                review: '',
                dateRated: new Date().toISOString()
            }

            try
            {
                await addRating(ratingRequest)

                const updatedRating = await getUserRating(currentCounterparty)
                setCounterpartyRatings((prevRatings) => ({
                    ...prevRatings,
                    [currentCounterparty]: updatedRating.averageRating
                }))
                setModalVisible(false)
            } catch (error)
            {
                console.error('Failed to add rating:', error)
            }
        }
    }

    const openRatingModal = (counterpartyId: string, transaction: TransactionHistory) =>
    {
        setCurrentCounterparty(counterpartyId)
        setCurrentTransaction(transaction)
        setModalVisible(true)
    }

    const renderStars = (rating: number) =>
    {
        const stars = []
        for (let i = 1; i <= 5; i++)
        {
            stars.push(
                <TouchableOpacity key={i} onPress={() => setRating(i)}>
                    <FontAwesome
                        name={i <= rating ? "star" : "star-o"}
                        size={30}
                        color={i <= rating ? "#FFD700" : "#ccc"}
                    />
                </TouchableOpacity>
            )
        }
        return <View style={styles.stars}>{stars}</View>
    }

    const renderItem = ({item}: {item: TransactionHistory}) =>
    {
        const isSeller = item.sellerId === userId
        const roleMessage = isSeller ? 'You were selling this product' : 'You were buying this product'
        const counterpartyId = isSeller ? item.buyerId : item.sellerId
        const counterpartyUsername = isSeller ? item.buyerUsername : item.sellerUsername
        const counterpartyRating = counterpartyRatings[counterpartyId]

        return (
            <View style={styles.transactionItem}>
                <Image source={{uri: item.productImage}} style={styles.productImage} />
                <View style={styles.transactionDetails}>
                    <Text style={styles.roleMessage}>{roleMessage}:</Text>
                    <Text style={styles.label}>Product: <Text style={styles.text}>{item.productName}</Text></Text>
                    <Text style={styles.label}>Description: <Text style={styles.text}>{item.productDescription}</Text></Text>
                    <Text style={styles.label}>Counterparty: <Text style={styles.text}>{counterpartyUsername}</Text></Text>
                    {counterpartyRating !== null && counterpartyRating !== undefined ? (
                        <Text style={styles.label}>Counterparty Rating: <Text style={styles.text}>{counterpartyRating.toFixed(2)}</Text></Text>
                    ) : (
                        <Text style={styles.label}>Counterparty Rating: <Text style={styles.text}>No rating yet</Text></Text>
                    )}
                    <Text style={styles.label}>Date: <Text style={styles.text}>{new Date(item.dateCompleted).toLocaleDateString()}</Text></Text>
                    <TouchableOpacity
                        style={styles.minimalButton}
                        onPress={() => openRatingModal(counterpartyId, item)}
                    >
                        <Text style={styles.minimalButtonText}>Rate Counterparty</Text>
                    </TouchableOpacity>
                </View>
            </View>
        )
    }

    if (loading)
    {
        return <Text>Loading...</Text>
    }

    if (error)
    {
        return <Text>Error: {error}</Text>
    }

    return (
        <View style={styles.container}>
            <FlatList
                data={userTransactions}
                keyExtractor={(item) => item.transactionId.toString()}
                renderItem={renderItem}
                ListEmptyComponent={<Text>No transactions found.</Text>}
            />
            <Modal
                animationType="slide"
                transparent={true}
                visible={modalVisible}
                onRequestClose={() => setModalVisible(false)}
            >
                <View style={styles.modalView}>
                    <Text style={styles.modalText}>Rate Counterparty</Text>
                    {renderStars(rating)}
                    <View style={styles.buttonRow}>
                        <TouchableOpacity
                            style={[buttonStyles.button, buttonStyles.blueButton]}
                            onPress={handleRateCounterparty}
                        >
                            <Text style={[buttonStyles.buttonText, buttonStyles.blueButtonText]}>Submit</Text>
                        </TouchableOpacity>
                        <TouchableOpacity
                            style={[buttonStyles.button, buttonStyles.redButton]}
                            onPress={() => setModalVisible(false)}
                        >
                            <Text style={[buttonStyles.buttonText, buttonStyles.redButtonText]}>Cancel</Text>
                        </TouchableOpacity>
                    </View>
                </View>
            </Modal>
        </View>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 16,
        backgroundColor: '#fff',
    },
    transactionItem: {
        flexDirection: 'row',
        padding: 10,
        borderBottomWidth: 1,
        borderBottomColor: '#ddd',
        alignItems: 'center',
        marginBottom: 10,
    },
    productImage: {
        width: 60,
        height: 60,
        borderRadius: 5,
        marginRight: 16,
    },
    transactionDetails: {
        flex: 1,
    },
    roleMessage: {
        fontSize: 16,
        fontStyle: 'italic',
        color: '#333',
        marginBottom: 8,
    },
    label: {
        fontSize: 14,
        fontWeight: 'bold',
        color: '#444',
        marginBottom: 2,
    },
    text: {
        fontWeight: 'normal',
        color: '#555',
    },
    minimalButton: {
        marginTop: 10,
        paddingVertical: 6,
        paddingHorizontal: 12,
        backgroundColor: '#F8F8F8',
        borderRadius: 12,
        borderWidth: 1,
        borderColor: '#DDDDDD',
        alignSelf: 'flex-start',
    },
    minimalButtonText: {
        color: '#333333',
        fontSize: 12,
        fontWeight: '600',
    },
    stars: {
        flexDirection: 'row',
        marginBottom: 15,
    },
    modalView: {
        margin: 20,
        backgroundColor: 'white',
        borderRadius: 20,
        padding: 35,
        alignItems: 'center',
        shadowColor: '#000',
        shadowOffset: {
            width: 0,
            height: 2,
        },
        shadowOpacity: 0.25,
        shadowRadius: 4,
        elevation: 5,
    },
    modalText: {
        marginBottom: 15,
        textAlign: 'center',
        fontSize: 18,
        fontWeight: 'bold',
    },
    buttonRow: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        width: '100%',
    },
})

export default MyTransactionsScreen