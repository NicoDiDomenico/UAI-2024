import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Toaster } from 'react-hot-toast';
import Layout from './components/Layout/Layout';
import Dashboard from './pages/Dashboard/Dashboard';
import Members from './pages/Members/Members';
import Classes from './pages/Classes/Classes';
import Trainers from './pages/Trainers/Trainers';
import Payments from './pages/Payments/Payments';
import MembershipPlans from './pages/MembershipPlans/MembershipPlans';

function App() {
  return (
    <BrowserRouter>
      <Toaster position="top-right" />
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Dashboard />} />
          <Route path="members" element={<Members />} />
          <Route path="classes" element={<Classes />} />
          <Route path="trainers" element={<Trainers />} />
          <Route path="payments" element={<Payments />} />
          <Route path="membership-plans" element={<MembershipPlans />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
