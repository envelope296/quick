import './App.css'

declare global {
  interface Window {
    WebApp: any;
  }
}

function App() {
  return (
	<>
	  <div>
		{window.WebApp}
	  </div>
	</>
  )
}

export default App
