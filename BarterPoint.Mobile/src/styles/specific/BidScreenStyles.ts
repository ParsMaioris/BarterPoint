import {StyleSheet} from 'react-native'

export const BidScreenStyles = StyleSheet.create({
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
    bidCard: {
        flexDirection: 'column',
        justifyContent: 'space-between',
        marginBottom: 16,
        padding: 16,
        backgroundColor: '#fff',
        borderRadius: 8,
        shadowColor: '#000',
        shadowOpacity: 0.1,
        shadowOffset: {width: 0, height: 2},
        shadowRadius: 8,
        elevation: 2,
    },
    productRow: {
        flexDirection: 'row',
        alignItems: 'center',
        marginBottom: 16,
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
    productDetailScreens: {
        flex: 1,
    },
    productName: {
        fontSize: 16,
        fontWeight: 'bold',
    },
    bidIcon: {
        marginHorizontal: 8,
    },
    actionRow: {
        flexDirection: 'row',
        justifyContent: 'space-around',
        borderTopWidth: 1,
        borderTopColor: '#e0e0e0',
        paddingTop: 8,
    },
    actionButton: {
        flexDirection: 'row',
        alignItems: 'center',
    },
    actionText: {
        marginLeft: 4,
        fontSize: 16,
    },
    deleteIcon: {
        marginHorizontal: 8,
    },
    approveIcon: {
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