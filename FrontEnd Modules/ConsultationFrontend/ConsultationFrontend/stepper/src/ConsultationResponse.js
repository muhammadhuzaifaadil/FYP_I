import React from 'react';
import { Button, Box, Typography } from '@mui/material';

const ConsultationResponse = ({ responseText, onRetry, onBackToHome }) => {
    return (
        <Box
            sx={{
                position: 'fixed',
                top: '50%',
                left: '50%',
                transform: 'translate(-50%, -50%)',
                backgroundColor: 'purple',
                color: 'white',
                padding: '20px',
                textAlign: 'center',
                borderRadius: '10px',
                maxWidth: '80%',
                zIndex: 1000
            }}
        >
            <Typography variant="h4" gutterBottom>
                Consultation Response
            </Typography>
            <Typography variant="body1" gutterBottom>
                {responseText}
            </Typography>
            <Button variant="contained" color="secondary" onClick={onRetry} sx={{ mr: 2 }}>
                Attempt Consultation Quiz Again
            </Button>
            <Button variant="contained" color="primary" onClick={onBackToHome}>
                Back to Home
            </Button>
        </Box>
    );
};

export default ConsultationResponse;
