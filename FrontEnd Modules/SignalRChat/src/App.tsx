import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { Login, ChatRoom } from './pages/_index';
import { Box, Container, Snackbar, Alert } from '@mui/material';
import { useState } from 'react';
import LoginPage from './pages/Login/LoginPage';
interface LoginResponse {
    userName: string;
}
const App = () => {
    const [alertOpen, setAlertOpen] = useState(false);
    const [userData, setUserData] = useState<LoginResponse | null>(null);

    // Function to receive userData from LoginPage
    const handleLogin = (data: LoginResponse) => {
        setUserData(data);
    };

    return (
        <Router>
            <Box sx={{ width: '100%', height: '100vh' }}>
                <Container>
                    <Box sx={{
                        minWidth: '100%', minHeight: '100vh', padding: '20px',
                        display: 'flex', flexDirection: 'column', justifyContent: 'center', alignItems: 'center',
                    }}>
                        <Switch>
                            <Route exact path='/'>
                                {/* Pass handleLogin function to LoginPage */}
                                <LoginPage onLogin={handleLogin} />
                            </Route> 
                            <Route path="/login">
                                {/* Pass userData to Login component */}
                                <Login userData={userData} />
                            </Route>
                            <Route path="/chat">
                                <ChatRoom />
                            </Route>
                        </Switch>
                    </Box>
                </Container>
            </Box>

            <Snackbar open={alertOpen} autoHideDuration={1500}>
                <Alert severity="error" sx={{ width: '100%' }} variant="filled">
                    Couldn't join the room!
                </Alert>
            </Snackbar>
        </Router>
    );
};

export default App;