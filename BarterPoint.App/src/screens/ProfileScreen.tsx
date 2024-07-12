import React from 'react'
import {View, Text, StyleSheet, Image, TouchableOpacity, Alert} from 'react-native'
import {useDispatch, useSelector} from 'react-redux'
import {RootState, AppDispatch} from '../redux/Store'
import {clearCurrentUser} from '../redux/slices/UserSlice'
import {buttonStyles} from '../styles/common/ButtonStyles'
import {useNavigation} from '@react-navigation/native'
import {mockProfileImage} from '../mocks/Mock'
import {StackNavigationProp} from '@react-navigation/stack'
import {RootStackParamList} from '../navigation/navigationTypes'
import MyProductsScreen from './MyProductsScreen'

type ProductListScreenNavigationProp = StackNavigationProp<RootStackParamList, 'SignIn'>


const ProfileScreen: React.FC = () =>
{
  const dispatch = useDispatch<AppDispatch>()
  const navigation = useNavigation<ProductListScreenNavigationProp>()
  const currentUser = useSelector((state: RootState) => state.users.currentUser)

  const handleLogout = async () =>
  {
    try
    {
      dispatch(clearCurrentUser())
      navigation.navigate('SignIn')
    } catch (error)
    {
      console.error('Logout error:', error)
      Alert.alert('Error', 'An error occurred while logging out')
    }
  }

  const handleProfileImage = () =>
  {
    Alert.alert('Change Profile Image', 'Profile image change functionality to be implemented.')
  }

  const renderHeader = () => (
    <View style={styles.header}>
      <TouchableOpacity onLongPress={handleProfileImage}>
        <Image source={{uri: mockProfileImage}} style={styles.profileImage} />
      </TouchableOpacity>
      <Text style={styles.title}>
        Welcome{currentUser?.id ? `, ${currentUser.id}` : ''}
      </Text>
    </View>
  )

  return (
    <View style={styles.container}>
      {renderHeader()}

      <View style={styles.productsHeader}>
        <Text style={styles.productsTitle}>Your Products</Text>
      </View>

      <View style={styles.productsContainer}>
        <MyProductsScreen />
      </View>

      <View style={styles.buttonContainer}>
        <TouchableOpacity style={buttonStyles.button} onPress={handleLogout}>
          <Text style={buttonStyles.buttonText}>Logout</Text>
        </TouchableOpacity>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#F5F5F5',
  },
  header: {
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    paddingVertical: 20,
    marginBottom: 10,
    shadowColor: '#000',
    shadowOffset: {width: 0, height: 2},
    shadowOpacity: 0.1,
    shadowRadius: 5,
    elevation: 5,
  },
  profileImage: {
    width: 100,
    height: 100,
    borderRadius: 50,
    marginBottom: 10,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    color: '#333333',
  },
  productsHeader: {
    paddingVertical: 15,
    paddingHorizontal: 20,
    backgroundColor: '#FFFFFF',
    borderBottomWidth: 1,
    borderBottomColor: '#DDD',
    alignItems: 'center',
  },
  productsTitle: {
    fontSize: 22,
    fontWeight: 'bold',
    color: '#333333',
  },
  productsContainer: {
    flex: 1,
    padding: 10,
  },
  buttonContainer: {
    marginVertical: 20,
    alignItems: 'center',
  },
  errorText: {
    color: 'red',
    fontSize: 16,
  },
})

export default ProfileScreen