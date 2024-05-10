import React, { useState } from 'react';
import { useHistory } from 'react-router-dom'; // Import useHistory for navigation
import axios from 'axios';
import Login from './Login';

interface LoginResponse {
  userName: string;
}

interface LoginPageProps {
  onLogin: (data: LoginResponse) => void; // Define the type of onLogin prop
}

const LoginPage: React.FC<LoginPageProps> = ({ onLogin }) => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [userData, setUserData] = useState<LoginResponse | null>(null);
  const history = useHistory(); // Initialize useHistory hook
  const [alertOpen, setAlertOpen] = useState(false);

  const handleLogin = async () => {
    try {
      const response = await axios.post<LoginResponse>('http://localhost:5100/api/Auth/login', {
        userEmail: email,
        password: password,
      });
      setUserData(response.data);
      setError('');
      
      // Redirect to /login route upon successful login
      history.push('/login');

      // Call the onLogin function with the response data
      onLogin(response.data);

    } catch (error) {
      setError('Invalid email or password');
      setUserData(null);
    }
  };

  return (
    <div>
      <h2>Login</h2>
      <div>
        <label>Email:</label>
        <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
      </div>
      <div>
        <label>Password:</label>
        <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
      </div>
      <button onClick={handleLogin}>Login</button>
      {error && <div style={{ color: 'red' }}>{error}</div>}
      {userData && <Login userData={userData} />}
    </div>
  );
};

export default LoginPage;