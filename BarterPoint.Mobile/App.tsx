import React, {useEffect, useState} from 'react'
import {NavigationContainer} from '@react-navigation/native'
import {createNativeStackNavigator} from '@react-navigation/native-stack'
import {Provider} from 'react-redux'
import AsyncStorage from '@react-native-async-storage/async-storage'
import SignInScreen from './src/screens/SignInScreen'
import ProfileScreen from './src/screens/ProfileScreen'
import LandingPageScreen from './src/screens/LandingPageScreen'
import PostItemScreen from './src/screens/PostItemScreen'
import ProductListScreen from './src/screens/ProductListScreen'
import ProductDetailScreen from './src/screens/ProductDetailScreen'
import BidScreen from './src/screens/BidScreen'
import CreateAccountScreen from './src/screens/CreateAccountScreen'
import ProductsScreen from './src/screens/ProductsScreen'
import store from './src/redux/Store'
import {setUserId} from './src/redux/slices/UserSlice'

const Stack = createNativeStackNavigator()

const App = () =>
{
  const [initialRouteName, setInitialRouteName] = useState('SignIn')
  const [isTokenChecked, setIsTokenChecked] = useState(false)

  useEffect(() =>
  {
    const checkLoginState = async () =>
    {
      const userId = await AsyncStorage.getItem('userId')
      if (userId)
      {
        store.dispatch(setUserId(userId))
        setInitialRouteName('LandingPageScreen')
      }
      setIsTokenChecked(true)
    }

    checkLoginState()
  }, [])

  if (!isTokenChecked)
  {
    return null
  }

  return (
    <Provider store={store}>
      <NavigationContainer>
        <Stack.Navigator initialRouteName={initialRouteName}>
          <Stack.Screen name="SignIn" component={SignInScreen} options={{title: 'Welcome Back!'}} />
          <Stack.Screen name="CreateAccount" component={CreateAccountScreen} options={{title: 'Create Account'}} />
          <Stack.Screen name="LandingPageScreen" component={LandingPageScreen} options={{title: 'Welcome to BarterApp'}} />
          <Stack.Screen name="PostItemScreen" component={PostItemScreen} options={{title: 'Post a New Item'}} />
          <Stack.Screen name="Profile" component={ProfileScreen} options={{title: 'Your Profile'}} />
          <Stack.Screen name="ProductListScreen" component={ProductListScreen} options={{title: 'Product Listings'}} />
          <Stack.Screen name="ProductDetailScreen" component={ProductDetailScreen} options={{title: 'Product Detail'}} />
          <Stack.Screen name="ProductsScreen" component={ProductsScreen} options={{title: 'Products'}} />
          <Stack.Screen name="BidScreen" component={BidScreen} options={{title: 'Bids'}} />
        </Stack.Navigator>
      </NavigationContainer>
    </Provider>
  )
}

export default App