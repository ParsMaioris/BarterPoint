import React, {useState} from 'react'
import {View, Text, TextInput, TouchableOpacity, StyleSheet, Alert} from 'react-native'
import {useDispatch} from 'react-redux'
import {useNavigation} from '@react-navigation/native'
import {signInUser} from '../api/ApiService'
import {AppDispatch} from '../redux/Store'
import {RootStackParamList} from '../navigation/navigationTypes'
import {StackNavigationProp} from '@react-navigation/stack'
import CryptoJS from 'crypto-js'
type ProductListScreenNavigationProp = StackNavigationProp<RootStackParamList, 'CreateAccount'>

const SignInScreen: React.FC = () =>
{
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const dispatch = useDispatch<AppDispatch>()
  const navigation = useNavigation<ProductListScreenNavigationProp>()

  const handleSignIn = async () =>
  {
    const passwordHash = CryptoJS.SHA256(password).toString(CryptoJS.enc.Hex)
    const payload = {username, passwordHash: passwordHash}

    try
    {
      const resultAction = await dispatch(signInUser(payload))
      console.log(resultAction)

      if (signInUser.fulfilled.match(resultAction))
      {
        Alert.alert('Success', 'Signed in successfully')
        navigation.navigate('LandingPageScreen')
      } else
      {
        Alert.alert('Error', resultAction.payload || 'Failed to sign in')
      }
    } catch (error)
    {
      Alert.alert('Error', 'An error occurred while signing in')
    }
  }

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Sign In</Text>
      <TextInput
        style={styles.input}
        placeholder="Username"
        placeholderTextColor="#888"
        value={username}
        onChangeText={setUsername}
      />
      <TextInput
        style={styles.input}
        placeholder="Password"
        placeholderTextColor="#888"
        value={password}
        onChangeText={setPassword}
        secureTextEntry
      />
      <TouchableOpacity style={styles.signInButton} onPress={handleSignIn}>
        <Text style={styles.buttonText}>Sign In</Text>
      </TouchableOpacity>
      <TouchableOpacity
        style={styles.createAccountButton}
        onPress={() => navigation.navigate('CreateAccount')}
      >
        <Text style={styles.buttonText}>Create Account</Text>
      </TouchableOpacity>
    </View>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 16,
    backgroundColor: '#F5F5F5',
  },
  title: {
    fontSize: 32,
    fontWeight: 'bold',
    marginBottom: 40,
    color: '#333',
  },
  input: {
    width: '100%',
    height: 50,
    borderColor: '#DDD',
    borderWidth: 1,
    borderRadius: 8,
    paddingHorizontal: 16,
    marginBottom: 20,
    backgroundColor: '#FFF',
    fontSize: 16,
  },
  signInButton: {
    width: '100%',
    height: 50,
    backgroundColor: '#007AFF',
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 8,
    marginBottom: 20,
  },
  createAccountButton: {
    width: '100%',
    height: 50,
    backgroundColor: '#34C759',
    justifyContent: 'center',
    alignItems: 'center',
    borderRadius: 8,
  },
  buttonText: {
    color: '#FFF',
    fontSize: 18,
    fontWeight: 'bold',
  },
})

export default SignInScreen