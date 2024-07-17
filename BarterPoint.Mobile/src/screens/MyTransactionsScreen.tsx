import React, {useEffect} from 'react'
import {View, Text, FlatList, StyleSheet} from 'react-native'
import {useSelector, useDispatch} from 'react-redux'
import {RootState, AppDispatch} from '../redux/Store'
import {getUserTransactions} from '../api/ApiService'
import {TransactionHistory} from '../models/TransactionHistory'

const MyTransactionsScreen: React.FC = () =>
{
    const dispatch = useDispatch<AppDispatch>()
    const userId = useSelector((state: RootState) => state.users.userId)
    const userTransactions = useSelector((state: RootState) => state.transactions.user)
    const loading = useSelector((state: RootState) => state.transactions.loading)
    const error = useSelector((state: RootState) => state.transactions.error)

    useEffect(() =>
    {
        if (userId)
        {
            dispatch(getUserTransactions(userId))
        }
    }, [dispatch, userId])

    const renderItem = ({item}: {item: TransactionHistory}) =>
    {
        const isSeller = item.sellerId === userId
        const roleMessage = isSeller ? 'You were selling' : 'You were buying'
        const counterparty = isSeller ? item.buyerUsername : item.sellerUsername

        return (
            <View style={styles.transactionItem}>
                <View style={styles.transactionDetails}>
                    <Text style={styles.transactionText}>{roleMessage} this product:</Text>
                    <Text style={styles.transactionText}>Product: {item.productName}</Text>
                    <Text style={styles.transactionText}>Counterparty: {counterparty}</Text>
                    <Text style={styles.transactionText}>Date: {new Date(item.dateCompleted).toLocaleDateString()}</Text>
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
    transactionItem: {
        flexDirection: 'row',
        padding: 16,
        borderBottomWidth: 1,
        borderBottomColor: '#ddd',
    },
    transactionDetails: {
        flex: 1,
    },
    transactionText: {
        fontSize: 16,
        marginBottom: 4,
    },
})

export default MyTransactionsScreen