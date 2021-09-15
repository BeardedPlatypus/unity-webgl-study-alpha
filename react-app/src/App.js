import React from "react";
import Unity, { UnityContext } from "react-unity-webgl"


const unityContext = new UnityContext({
  loaderUrl: "unity/web-study-build.loader.js",
  dataUrl: "unity/web-study-build.data",
  frameworkUrl: "unity/web-study-build.framework.js",
  codeUrl: "unity/web-study-build.wasm",
});

const unityStyle = {
  height: "100%",
  width: 950,
  border: "2px solid black",
  background: "grey",
}


function App() {
  return (
    <div className="App">
      <header className="App-header">
        <div style={{
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
        }}>
          <h1>
            Unity Web Study #1.
          </h1>
        </div>
        <div style={{
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
        }}>
          <Unity
            unityContext={unityContext}
            style={unityStyle}
          />
        </div>
      </header>
    </div>
  );
}

export default App;
