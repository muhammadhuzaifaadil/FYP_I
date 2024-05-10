import './App.css';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

import Question from './components/Question';
import Result from './components/Result';
import { ContextProvider } from './Hooks/UseStateContext';

function App() {
  return (
    <BrowserRouter>
      <ContextProvider> {/* Wrap all routes with ContextProvider */}
        <Routes>
          <Route path="/question" element={<Question />} />
          <Route path="/result" element={<Result />} />
        </Routes>
      </ContextProvider>
    </BrowserRouter>
  );
}

export default App;
