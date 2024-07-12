import React, {useState} from "react"
import {Auth} from "aws-amplify"
import
{
  View,
  TextInput,
  Button,
  Text,
  StyleSheet,
  TouchableOpacity,
  ScrollView,
  KeyboardAvoidingView,
  Platform,
  TouchableWithoutFeedback,
  Keyboard,
  Alert,
} from "react-native"
import {SignUpScreenNavigationProp} from "models/navigationTypes"

type SignUpScreenProps = {
  navigation: SignUpScreenNavigationProp
}

const CreateAccountScreen1: React.FC<SignUpScreenProps> = ({navigation}) =>
{
  const [email, setEmail] = useState("")
  const [isEmailValid, setIsEmailValid] = useState(true)
  const [phoneNumber, setPhoneNumber] = useState("")
  const [isPhoneNumberValid, setIsPhoneNumberValid] = useState(true)
  const [password, setPassword] = useState("")
  const [confirmPassword, setConfirmPassword] = useState("")
  const [passwordsMatch, setPasswordsMatch] = useState(true)

  const handleSignUp = async () =>
  {
    const formattedPhoneNumber = `+1${phoneNumber}`

    try
    {
      const signUpResponse = await Auth.signUp({
        username: email,
        password,
        attributes: {
          email,
          phone_number: formattedPhoneNumber,
        },
      })
      navigation.navigate("ConfirmationScreen", {username: email})
      console.log("Sign up success", signUpResponse)
    } catch (error)
    {
      if (error instanceof Error && error.name === "UsernameExistsException")
      {
        // The user already exists, handle resending the code
        try
        {
          await Auth.resendSignUp(email)
          navigation.navigate("ConfirmationScreen", {username: email})
          Alert.alert(
            "Confirmation Code Sent",
            "A new confirmation code has been sent to your email."
          )
        } catch (resendError)
        {
          // Handle errors in resending the code if any
          if (resendError instanceof Error)
          {
            Alert.alert(
              "Error",
              resendError.message || "Failed to resend the confirmation code."
            )
          }
        }
      } else
      {
        // Handle other sign-up errors
        if (error instanceof Error)
        {
          Alert.alert(
            "Error",
            error.message || "An error occurred during sign up."
          )
        }
      }
    }
  }

  const validateEmail = (email: string) =>
  {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    const isValid = emailRegex.test(email)
    setIsEmailValid(isValid)
    return isValid
  }

  const validatePhoneNumber = (phoneNumber: string) =>
  {
    const numericPhoneNumber = phoneNumber.replace(/\D/g, "")
    const isValid = numericPhoneNumber.length === 10
    setIsPhoneNumberValid(isValid)
    return isValid
  }

  const handlePasswordChange = (newPassword: string) =>
  {
    setPassword(newPassword)
    setPasswordsMatch(newPassword === confirmPassword)
  }

  const handleConfirmPasswordChange = (newConfirmPassword: string) =>
  {
    setConfirmPassword(newConfirmPassword)
    setPasswordsMatch(newConfirmPassword === password)
  }

  return (
    <ScrollView contentContainerStyle={styles.innerContainer}>
      <TouchableWithoutFeedback onPress={Keyboard.dismiss}>
        <View style={styles.formContainer}>
          <View style={styles.input}>
            <TextInput
              value={email}
              placeholder="Email"
              onChangeText={(text) =>
              {
                setEmail(text)
                validateEmail(text)
              }}
              placeholderTextColor="#888"
              style={[
                styles.passwordInputField,
                !isEmailValid && styles.invalidInput,
              ]}
              onBlur={() => validateEmail(email)}
              testID="email-address"
              keyboardType="email-address"
              autoCapitalize="none"
            />
            {!isEmailValid && (
              <Text style={styles.errorMessage}>Invalid email address</Text>
            )}
          </View>
          <View style={styles.input}>
            <TextInput
              value={phoneNumber}
              placeholder="Phone Number"
              onChangeText={(text) =>
              {
                const numericText = text.replace(/\D/g, "")
                setPhoneNumber(numericText)
                validatePhoneNumber(numericText)
              }}
              placeholderTextColor="#888"
              style={[
                styles.passwordInputField,
                !isPhoneNumberValid && styles.invalidInput,
              ]}
              onBlur={() => validatePhoneNumber(phoneNumber)}
              keyboardType="numeric"
              testID="input-number"
              maxLength={10}
            />
            {!isPhoneNumberValid && (
              <Text style={styles.errorMessage}>Invalid phone number</Text>
            )}
          </View>
          <View style={styles.input}>
            <TextInput
              placeholder="Password"
              onChangeText={(text) =>
              {
                setPassword(text)
                handlePasswordChange(text)
              }}
              placeholderTextColor="#888"
              style={[
                styles.passwordInputField,
                !passwordsMatch && styles.invalidInput,
              ]}
              secureTextEntry
              testID="input-password"
            />
          </View>
          <View style={styles.input}>
            <TextInput
              placeholder="Confirm Password"
              value={confirmPassword}
              onChangeText={(text) =>
              {
                setConfirmPassword(text)
                handleConfirmPasswordChange(text)
              }}
              placeholderTextColor="#888"
              style={[
                styles.passwordInputField,
                !passwordsMatch && styles.invalidInput,
              ]}
              secureTextEntry
              testID="input-confirm-password"
            />
            {!passwordsMatch && (
              <Text style={styles.errorMessage}>Passwords do not match</Text>
            )}
          </View>

          <Button title="Sign Up" onPress={handleSignUp} />

          <TouchableOpacity onPress={() => navigation.navigate("SignIn")}>
            <Text style={styles.link}>Sign In</Text>
          </TouchableOpacity>
        </View>
      </TouchableWithoutFeedback>
    </ScrollView>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    alignContent: "center",
    justifyContent: "center",
  },
  formContainer: {
    width: "80%",
    alignItems: "center",
  },
  innerContainer: {
    flexGrow: 1,
    justifyContent: "center",
    alignItems: "center",
  },
  input: {
    width: "100%",
    padding: 10,
    marginVertical: 10,
    borderWidth: 1, // Border for the outer View
    borderColor: "gray",
    borderRadius: 5,
    // Remove any additional border styling from here that applies to the TextInput
  },
  passwordInputField: {
    height: 40,
  },
  link: {
    color: "blue",
    marginTop: 15,
    textDecorationLine: "underline",
  },
  invalidInput: {
    borderWidth: 1, // Only add borderWidth if you want a border on invalid input
    borderColor: "red",
  },
  errorMessage: {
    color: "red",
    fontSize: 12,
  },
})

export default CreateAccountScreen1
