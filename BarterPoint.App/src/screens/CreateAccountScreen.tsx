import React, {useState} from 'react'
import {View, Text, TextInput, TouchableOpacity, StyleSheet, Alert} from 'react-native'
import {useDispatch} from 'react-redux'
import {useNavigation} from '@react-navigation/native'
import {AppDispatch} from '../redux/Store'
import {registerUser} from '../api/ApiService'
import {StackNavigationProp} from '@react-navigation/stack'
import {RootStackParamList} from '../navigation/navigationTypes'

type ProductListNavigationProp = StackNavigationProp<RootStackParamList, 'SignIn'>

const CreateAccountScreen: React.FC = () =>
{
    const [username, setUsername] = useState('')
    const [password, setPassword] = useState('')
    const [email, setEmail] = useState('')
    const [name, setName] = useState('')
    const [location, setLocation] = useState('')
    const dispatch = useDispatch<AppDispatch>()
    const navigation = useNavigation<ProductListNavigationProp>()

    const handleSignUp = async () =>
    {
        const dateJoined = new Date().toISOString().split('T')[0]
        const payload = {username, passwordHash: password, email, name, location, dateJoined}

        try
        {
            const resultAction = await dispatch(registerUser(payload))
            if (registerUser.fulfilled.match(resultAction))
            {
                Alert.alert('Success', 'Account created successfully')
                navigation.navigate('SignIn')
            } else
            {
                Alert.alert('Error', resultAction.payload || 'Failed to create account')
            }
        } catch (error)
        {
            Alert.alert('Error', 'An error occurred while creating the account')
        }
    }

    return (
        <View style={styles.container}>
            <Text style={styles.title}>Create a New Account</Text>
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
            <TextInput
                style={styles.input}
                placeholder="Email"
                placeholderTextColor="#888"
                value={email}
                onChangeText={setEmail}
            />
            <TextInput
                style={styles.input}
                placeholder="Name"
                placeholderTextColor="#888"
                value={name}
                onChangeText={setName}
            />
            <TextInput
                style={styles.input}
                placeholder="Location"
                placeholderTextColor="#888"
                value={location}
                onChangeText={setLocation}
            />
            <TouchableOpacity style={styles.signUpButton} onPress={handleSignUp}>
                <Text style={styles.buttonText}>Sign Up</Text>
            </TouchableOpacity>
            <TouchableOpacity
                style={styles.backToSignInButton}
                onPress={() => navigation.navigate('SignIn')}
            >
                <Text style={styles.buttonText}>Back to Sign In</Text>
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
    signUpButton: {
        width: '100%',
        height: 50,
        backgroundColor: '#007AFF',
        justifyContent: 'center',
        alignItems: 'center',
        borderRadius: 8,
        marginBottom: 20,
    },
    backToSignInButton: {
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

export default CreateAccountScreen