import { BrowserRouter, Routes, Route } from "react-router-dom"
import { Header } from "./layout/Header/Header"
import { Footer } from "./layout/Footer/Footer";
import './index.scss';

function App() {

  const mainstyle = {
    width: '25px',
    height: '300px'
  };

  return (
    <>
      <BrowserRouter>
      <Header/>
      <main style={mainstyle}>
        <Routes>
          <Route path='/' element={<div>ENVELOPE</div>}></Route>
          <Route path='*' element={<div>ENVELOPE не найден :(</div>}></Route>
        </Routes>
      </main>
      <Footer/>
      </BrowserRouter>
    </>
  )
}
export default App
