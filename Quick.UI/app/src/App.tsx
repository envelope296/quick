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
		{window.WebApp.initData}
	  </div>
	</>
  )
}

export default App
