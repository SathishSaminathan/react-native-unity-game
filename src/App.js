/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 *
 * @format
 * @flow
 */

import React from 'react';
import {StyleSheet, View, Text, StatusBar, Button} from 'react-native';

import {
  Header,
  LearnMoreLinks,
  Colors,
  DebugInstructions,
  ReloadInstructions,
} from 'react-native/Libraries/NewAppScreen';
import {UnityView, MessageHandler, UnityModule} from 'react-native-unity-view';

class App extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      unityPaused: false,
    };
  }

  onPauseAndResumeUnity() {
    if (this.state.unityPaused) {
      UnityModule.resume();
    } else {
      UnityModule.pause();
    }

    this.setState({
      unityPaused: !this.state.unityPaused,
    });
  }

  onUnityMessage() {}

  render() {
    const {unityPaused} = this.state;

    let unityElement = (
      <UnityView
        style={{position: 'absolute', left: 0, right: 0, top: 0, bottom: 0}}
        onUnityMessage={this.onUnityMessage.bind(this)}
      />
    );

    return <>{unityElement}</>;
  }
}

const styles = StyleSheet.create({
  container: {
    position: 'absolute',
    top: 0,
    bottom: 0,
    left: 0,
    right: 0,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#535353',
  },
  welcome: {
    fontSize: 20,
    textAlign: 'center',
    margin: 10,
  },
  button: {
    marginTop: 10,
  },
});

export default App;
