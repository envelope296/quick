import { BrowserRouter, Route, Routes } from "react-router-dom";
import { TestMain } from "@/components/pages/TestMain";
import { TestChild } from "./components/pages/TestChild";

export function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/*" element={<TestMain />} />
        <Route path="/child/*" element={<TestChild />} />
      </Routes>
    </BrowserRouter>
  );
}