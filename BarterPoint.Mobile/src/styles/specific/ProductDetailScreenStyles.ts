import {StyleSheet} from 'react-native'

export const ProductDetailScreenStyles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: "#fff",
        padding: 20,
    },
    productImage: {
        width: "100%",
        height: 200,
        resizeMode: "cover",
        marginBottom: 20,
    },
    productName: {
        fontSize: 24,
        fontWeight: "bold",
    },
    productDescription: {
        fontSize: 16,
        marginVertical: 10,
    },
    tradeFor: {
        fontSize: 16,
        fontWeight: "bold",
        marginVertical: 10,
    },
    selectText: {
        fontSize: 16,
        marginVertical: 10,
    },
    userItem: {
        marginRight: 10,
        padding: 10,
        borderWidth: 1,
        borderColor: "#ccc",
        borderRadius: 10,
        alignItems: "center",
    },
    selectedItem: {
        borderColor: "#000",
    },
    userImage: {
        width: 100,
        height: 100,
        borderRadius: 10,
    },
    userName: {
        marginTop: 10,
        fontSize: 14,
        textAlign: "center",
    },
    button: {
        backgroundColor: "#007BFF",
        paddingVertical: 10,
        paddingHorizontal: 15,
        borderRadius: 5,
        borderWidth: 1,
        borderColor: "#007BFF",
        alignItems: "center",
        justifyContent: "center",
        alignSelf: "center",
        marginTop: 20,
    },
    buttonText: {
        color: "#fff",
        fontSize: 16,
        fontWeight: "600",
    },
    header: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginBottom: 20,
    },
})