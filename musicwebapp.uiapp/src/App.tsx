import { Navigate, Route, Routes } from "react-router-dom";
import LoginRegisterPage from "./pages/LoginRegisterPage";
import MusicsPage from "./pages/MusicsPage";
import NotFound from "./components/NotFound/NotFound";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to={"/login-register"} />} />
      <Route path="/login-register" element={<LoginRegisterPage />} />
      <Route path="/musics" element={<MusicsPage />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
}

export default App;
