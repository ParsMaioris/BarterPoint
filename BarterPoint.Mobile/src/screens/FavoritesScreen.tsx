import React, {useEffect} from 'react'
import {useDispatch, useSelector} from 'react-redux'
import {FlatList, StyleSheet, Text, View, Image, TouchableOpacity, Platform, ToastAndroid, Alert} from 'react-native'
import {Ionicons} from '@expo/vector-icons'
import {AppDispatch, RootState} from '../redux/Store'
import {selectFavoriteProducts} from '../redux/Selectors'
import {getUserFavorites, removeFavorite} from '../api/ApiService'

const FavoritesScreen: React.FC = () =>
{
    const dispatch = useDispatch<AppDispatch>()
    const favorites = useSelector((state: RootState) => selectFavoriteProducts(state))
    const loading = useSelector((state: RootState) => state.users.status)
    const error = useSelector((state: RootState) => state.users.error)
    const userId = useSelector((state: RootState) => state.users.userId)

    useEffect(() =>
    {
        if (userId)
        {
            dispatch(getUserFavorites(userId))
        }
    }, [dispatch, userId])

    const handleRemoveFavorite = async (productId: string) =>
    {
        if (userId)
        {
            const resultAction = await dispatch(removeFavorite({userId, productId}))

            if (removeFavorite.fulfilled.match(resultAction))
            {
                if (Platform.OS === 'android')
                {
                    ToastAndroid.show('Product removed from favorites!', ToastAndroid.SHORT)
                } else
                {
                    Alert.alert('Success', 'Product removed from favorites!')
                }
            } else
            {
                const errorMessage = (resultAction.payload as string) || 'Failed to remove product from favorites.'
                if (Platform.OS === 'android')
                {
                    ToastAndroid.show(errorMessage, ToastAndroid.SHORT)
                } else
                {
                    Alert.alert('Error', errorMessage)
                }
            }
        }
    }

    if (loading === 'loading')
    {
        return <Text>Loading...</Text>
    }

    if (error)
    {
        return <Text>Error: {error}</Text>
    }

    if (favorites.length === 0)
    {
        return <Text style={styles.noFavorites}>No favorites yet</Text>
    }

    return (
        <View style={styles.container}>
            <FlatList
                data={favorites}
                keyExtractor={(item) => item.id.toString()}
                renderItem={({item}) => (
                    <View style={styles.favoriteItem}>
                        <Image source={{uri: item.image}} style={styles.favoriteImage} />
                        <View style={styles.favoriteDetails}>
                            <Text style={styles.favoriteName}>{item.name}</Text>
                            <Text style={styles.favoriteDescription}>{item.description}</Text>
                            <Text style={styles.favoriteInfo}><Text style={styles.bold}>Trade For:</Text> {item.tradeFor}</Text>
                            <Text style={styles.favoriteInfo}><Text style={styles.bold}>Condition:</Text> {item.condition}</Text>
                            <Text style={styles.favoriteInfo}><Text style={styles.bold}>Location:</Text> {item.location}</Text>
                        </View>
                        <TouchableOpacity style={styles.removeButton} onPress={() => handleRemoveFavorite(item.productId)}>
                            <Ionicons name="trash-outline" size={24} color="#7F7F7F" style={styles.removeIcon} />
                        </TouchableOpacity>
                    </View>
                )}
                contentContainerStyle={styles.flatListContent}
                showsVerticalScrollIndicator={false}
            />
        </View>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        backgroundColor: '#FFFFFF',
    },
    flatListContent: {
        paddingBottom: 20,
    },
    noFavorites: {
        textAlign: 'center',
        fontSize: 18,
        color: '#999',
        marginTop: 20,
    },
    favoriteItem: {
        backgroundColor: '#fff',
        borderRadius: 10,
        overflow: 'hidden',
        marginBottom: 20,
        borderWidth: 1,
        borderColor: '#ddd',
        shadowColor: '#000',
        shadowOffset: {
            width: 0,
            height: 2,
        },
        shadowOpacity: 0.1,
        shadowRadius: 4,
        elevation: 5,
    },
    favoriteImage: {
        width: '100%',
        height: 200,
        resizeMode: 'cover',
    },
    favoriteDetails: {
        padding: 16,
    },
    favoriteName: {
        fontSize: 18,
        fontWeight: 'bold',
        marginBottom: 8,
    },
    favoriteDescription: {
        marginBottom: 8,
    },
    favoriteInfo: {
        marginBottom: 4,
    },
    bold: {
        fontWeight: 'bold',
    },
    removeButton: {
        position: 'absolute',
        top: 10,
        right: 10,
    },
    removeIcon: {
        textShadowColor: '#000',
        textShadowOffset: {width: 1, height: 1},
        textShadowRadius: 1,
    },
})

export default FavoritesScreen