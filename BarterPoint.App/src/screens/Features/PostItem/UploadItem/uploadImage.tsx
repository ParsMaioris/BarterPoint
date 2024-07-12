import { Auth, Storage } from 'aws-amplify';
import { Alert } from 'react-native';

type UploadImageParams = {
  imageUri: string;
  imageType: 'profile' | 'item';
};

const UploadImage = async (params: UploadImageParams) => {

  try {
    // Fetch the current user ID
    const currentUser = await Auth.currentAuthenticatedUser();
    const userId = currentUser.attributes.sub;

    // Fetch the file from the local filesystem
    const response = await fetch(params.imageUri);
    const blob = await response.blob();
    const timestamp = new Date().toISOString(); // Use ISO string for uniqueness
    let key;
    if (params.imageType === 'profile') {
      key = `users/${userId}/profile/${timestamp}-${blob.size}.jpg`;
    } else {
      key = `users/${userId}/items/${timestamp}-${blob.size}.jpg`;
    }    

    // Upload the file to S3
    const uploadedImage = await Storage.put(key, blob, {
      contentType: 'image/jpeg', // Set the content type
    });

    return key;

  } catch (error) {
    console.error('Error uploading photo:', error);
    Alert.alert('Error', 'Failed to upload photo');
    return null;
  }
};

export default UploadImage;