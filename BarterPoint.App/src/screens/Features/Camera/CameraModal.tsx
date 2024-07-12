import React from 'react';
import { Modal } from 'react-native';
import CameraComponent from './Camera';
import { UploadSightingImageFormNavigationProp } from 'models/navigationTypes';

interface CameraModalProps {
    isVisible: boolean;
    onClose: () => void;
    navigator?: UploadSightingImageFormNavigationProp;
  }

  const CameraModal: React.FC<CameraModalProps> = ({ isVisible, onClose }) => {  
    return (
    <Modal visible={isVisible} onRequestClose={onClose}>
      <CameraComponent />
    </Modal>
  );
};

export default CameraModal;
