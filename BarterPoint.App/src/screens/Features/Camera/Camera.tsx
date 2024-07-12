import React, { useState, useEffect, useRef } from 'react';
import * as ImagePicker from 'expo-image-picker';
import { View, Image, Text, StyleSheet, TouchableOpacity, Alert } from 'react-native';
import { Camera, CameraType } from 'expo-camera';
import * as MediaLibrary from 'expo-media-library';
import * as Location from 'expo-location';
import { Magnetometer } from 'expo-sensors';
import { useNavigation } from '@react-navigation/native';
import { UploadSightingImageFormNavigationProp } from 'models/navigationTypes';

type CameraNavigationProp = {
  navigation: UploadSightingImageFormNavigationProp;
};

const CameraComponent: React.FC = () => {
  const [hasCameraPermission, setHasCameraPermission] = useState<null | boolean>(null);
  const [hasMediaLibraryPermission, setHasMediaLibraryPermission] = useState<null | boolean>(null);
  const [photoUri, setPhotoUri] = useState<string | null>(null);
  const [photoLocation, setPhotoLocation] = useState<Location.LocationObject | null>(null);
  const [heading, setHeading] = useState<number | null>(null);
  const [type, setType] = useState(CameraType.back);
  const cameraRef = useRef<Camera>(null);
  const navigation = useNavigation<UploadSightingImageFormNavigationProp>();

  useEffect(() => {
    async function requestPermissions() {
      const cameraStatus = await Camera.requestCameraPermissionsAsync();
      const mediaLibraryStatus = await MediaLibrary.requestPermissionsAsync();
      const locationStatus = await Location.requestForegroundPermissionsAsync();
      const { status: magnetometerStatus } = await Magnetometer.requestPermissionsAsync();
      setHasCameraPermission(cameraStatus.status === 'granted');
      setHasMediaLibraryPermission(mediaLibraryStatus.status === 'granted');
      Magnetometer.addListener((data) => {
        const { x, y } = data;
        setHeading(Math.atan2(y, x) * (180 / Math.PI));
      });
    }

    requestPermissions();
    return () => {
      Magnetometer.removeAllListeners();
    };
  }, []);

  const savePhotoWithWatermark = async () => {
    // Here, you would render the photo with the watermark and save it
    // This is a complex operation and might require a different approach
  };


  const toggleCameraType = () => {
    setType(type === CameraType.back ? CameraType.front : CameraType.back);
  };

  const savePhoto = async (photoUri: string, location: Location.LocationObject, heading: number) => {    
    if (!hasMediaLibraryPermission) {
      Alert.alert('Access denied', 'Sorry, we need camera roll permissions to make this work!');
      return;
    }
    try {
      const asset = await MediaLibrary.createAssetAsync(photoUri);
      console.log('Photo location:', location.coords, 'Heading:', heading);
      // Optionally, you can save location data here
      console.log('Photo location:', location.coords);
      await MediaLibrary.createAlbumAsync('YourAlbumName', asset, false);
    } catch (error) {
      console.error('Error saving photo:', error);
      Alert.alert('Error', 'Failed to save photo');
    }
  };

  const takePicture = async () => {
    if (cameraRef.current) {
      const photo = await cameraRef.current.takePictureAsync();
      const location = await Location.getCurrentPositionAsync({});
      savePhoto(photo.uri, location, heading ?? 0);
      setPhotoLocation(location);
      // Optionally save or process the photo with watermark here
      savePhotoWithWatermark();
      setPhotoUri(photo.uri);
      console.log('takePicture')
      navigation.navigate('UploadSightingImageForm', { photoUri: photo.uri });
    }
  };

  const selectImageFromGallery = async () => {
    let result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true,
      aspect: [4, 3],
      quality: 1,
    });
  
    if (!result.canceled) {
      console.log('result.canceled', result.assets);
      navigation.navigate('UploadSightingImageForm', {photoUri: ''});
    };
  }

  if (hasCameraPermission === null || hasMediaLibraryPermission === null) {
    return <View />;
  }
  if (!hasCameraPermission || !hasMediaLibraryPermission) {
    return <Text>No access to camera or media library</Text>;
  }

  return (
    <View style={styles.container}>
      {photoUri && (
        <View style={styles.watermarkContainer}>
          <Image source={{ uri: photoUri }} style={styles.photo} />
          <Text 
            style={styles.watermark}>
              Location: 
                {photoLocation?.coords.latitude}, 
                {photoLocation?.coords.longitude}, 
              Heading: 
                {heading}
          </Text>          
          <Text 
            style={styles.watermark}>
              Location: 
                {photoLocation?.coords.latitude}, 
                {photoLocation?.coords.longitude}, 
              Heading: 
                {heading}
            </Text>
        </View>
      )}
      <Camera style={styles.camera} type={type} ref={cameraRef}>
        {/* ... other camera components ... */}
      </Camera>
      <View style={styles.buttonContainer}>
        <TouchableOpacity style={styles.button} onPress={toggleCameraType}>
          <Text style={styles.text}>Flip</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.button} onPress={takePicture}>
          <Text style={styles.text}>Snap</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.button} onPress={selectImageFromGallery}>
          <Text style={styles.text}>Upload</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  camera: {
    flex: 1,
  },
  buttonContainer: {
    flex: 0.1,
    flexDirection: 'row',
    justifyContent: 'space-around',
    margin: 20,
  },
  button: {
    flex: 0.3,
    alignSelf: 'flex-end',
    alignItems: 'center',
    backgroundColor: '#fff', // You can style this as you like
    padding: 10,
    borderRadius: 5,
  },
  text: {
    fontSize: 18,
    color: 'black', // Choose color for the text
  },
  watermarkContainer: {
    position: 'absolute',
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    justifyContent: 'center',
    alignItems: 'center',
  },
  photo: {
    width: '100%',
    height: '100%',
  },
  watermark: {
    position: 'absolute',
    bottom: 10,
    right: 10,
    color: 'white',
    fontSize: 14,
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
    padding: 5,
  },
});

export default CameraComponent;
