// import React, { useState } from 'react';
// import { View, Text, TextInput, Image, Button, StyleSheet, Alert } from 'react-native';
// import { useRoute, RouteProp, useNavigation } from '@react-navigation/native';

// type UploadImageFormParamsList = {
//     UploadImageForm: {
//       photoUri: string;
//     };
//   };

// const UploadImageForm = () => {
//   const route = useRoute<RouteProp<UploadImageFormParamsList,'UploadImageForm'>>();
//   const navigation = useNavigation();
//   const { photoUri } = route.params; // Retrieve photoUri passed from previous screen

//   const [title, setTitle] = useState('');
//   const [description, setDescription] = useState('');

//   const handleSubmit = async () => {
//     Alert.alert('Success', 'Image and data uploaded successfully!');
//     navigation.goBack(); // Go back to the previous screen or navigate to another screen
//   };

//   return (
//     <View style={styles.container}>
//       {photoUri && <Image source={{ uri: photoUri }} style={styles.image} />}
//       <TextInput
//         style={styles.input}
//         placeholder="Title"
//         value={title}
//         onChangeText={setTitle}
//       />
//       <TextInput
//         style={styles.input}
//         placeholder="Description"
//         value={description}
//         onChangeText={setDescription}
//         multiline
//       />
//       <Button title="Submit" onPress={handleSubmit} />
//     </View>
//   );
// };

// const styles = StyleSheet.create({
//   container: {
//     flex: 1,
//     alignItems: 'center',
//     justifyContent: 'center',
//     padding: 20,
//   },
//   image: {
//     width: 300,
//     height: 200,
//     resizeMode: 'contain',
//     marginBottom: 20,
//   },
//   input: {
//     width: '100%',
//     borderWidth: 1,
//     borderColor: 'gray',
//     borderRadius: 5,
//     padding: 10,
//     marginBottom: 10,
//   },
// });

// export default UploadImageForm;
