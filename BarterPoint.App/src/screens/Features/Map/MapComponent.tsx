import React from 'react';
import { View, Text } from 'react-native';
import MapView, { Marker } from 'react-native-maps';
// import redMarker from '../../../../assets/images/redpp.png';
import redMarker from '../../../assets/images/redpp.png';
import blueMarker from '../../../assets/images/bluepp.png';
import bugMarker from '../../../assets/images/bugpp.png';

interface Sighting {
    id: number;
    latitude: number;
    longitude: number;
    title: string;
    description: string;
  }
  
  interface WildlifeMapProps {
    sightings: Sighting[];
  }

  const WildlifeMap: React.FC<WildlifeMapProps> = ({ sightings }) => {  
    let markerImage: any;
    var x = 3;
    if (x === 1) {
      markerImage = redMarker;
    } else if (x === 2) {
      markerImage = blueMarker;
    } else if (x === 3) {
      markerImage = bugMarker;
    }

    return (
      <View style={{ flex: 1, width: '100%' }}>
        <View style={{ flex: 1, width: '100%' }}>
        <MapView style={{ flex: 1, width: '100%' }}>
          {sightings.map(sighting => (
            
            <Marker
              key={sighting.id}
              coordinate={{ latitude: sighting.latitude, longitude: sighting.longitude }}
              title={sighting.title}
              description={sighting.description}
              image={markerImage} 
            />
          ))}
        </MapView>
      </View>
    </View>
  );
};

export default WildlifeMap;
