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
		{window.WebApp ? 'WebApp is available' : 'WebApp is not available'}
	  </div>
	</>
  )
}

export default App
