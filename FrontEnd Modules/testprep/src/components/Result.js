import React, { useEffect, useState } from 'react';
import { Alert, Button, Card, CardContent, CardMedia, Typography } from '@mui/material';
import { Box } from '@mui/system';
import { green, red, grey } from '@mui/material/colors';
import Answer from './Answer';
import { createAPIEndpoint, ENDPOINTS } from '../api';
import { getFormatedTime } from '../helper';
import useStateContext from '../Hooks/UseStateContext';
import { useNavigate } from 'react-router';

export default function Result() {
    const { context, setContext } = useStateContext();
    const [score, setScore] = useState(0);
    const [qnAnswers, setQnAnswers] = useState([]);
    const [showAlert, setShowAlert] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const ids = context.selectedOptions.map(x => x.qnId);
        createAPIEndpoint(ENDPOINTS.getAnswers)
            .post(ids)
            .then(res => {
                const qna = context.selectedOptions.map(selectedQn => {
                    const answerData = res.data.find(answer => answer.qnId === selectedQn.qnId);
                    return {
                        ...selectedQn,
                        ...answerData
                    };
                });
                setQnAnswers(qna);
                calculateScore(qna);
            })
            .catch(err => console.log(err));
    }, [context.selectedOptions]);

    const calculateScore = qna => {
      let tempScore = qna.reduce((acc, curr) => {
          // Convert answer and selected to integers for comparison
          const answer = parseInt(curr.answer);
          const selected = parseInt(curr.selected);
          // Compare answer and selected
          if (answer === selected || selected === null) {
              return acc + 1; // Correct answer or skipped question
          } else {
              return acc; // Incorrect answer
          }
      }, 0);
      setScore(tempScore);
  };
        // Calculate the maximum score based on the number of questions answered
    const maxScore = context.selectedOptions.length;

    const restart = () => {
        setContext({
            timeTaken: 0,
            selectedOptions: [] // Reset selectedOptions state
        });
        navigate("/question");
    };

    const submitScore = () => {
        createAPIEndpoint(ENDPOINTS.participant)
            .put(context.participantId, {
                participantId: context.participantId,
                score: score,
                timeTaken: context.timeTaken
            })
            .then(res => {
                setShowAlert(true);
                setTimeout(() => {
                    setShowAlert(false);
                }, 4000);
            })
            .catch(err => {
                console.log(err);
            });
    };

    return (
        <>
            <Card sx={{ mt: 5, display: 'flex', width: '100%', maxWidth: 640, mx: 'auto' }}>
                <Box sx={{ display: 'flex', flexDirection: 'column', flexGrow: 1 }}>
                    <CardContent sx={{ flex: '1 0 auto', textAlign: 'center' }}>
                        <Typography variant="h4">Test Completed!</Typography>
                        <Typography variant="h6">YOUR SCORE</Typography>
                        <Typography variant="h5" sx={{ fontWeight: 600 }}>
                            <Typography variant="span" color={green[500]}>
                                {score}
                            </Typography>/{maxScore}
                        </Typography>
                        <Typography variant="h6">Took {getFormatedTime(context.timeTaken) + ' mins'}</Typography>

                        <Button variant="contained" sx={{ mx: 1 }} size="small" onClick={restart}>
                            Re-try
                        </Button>
                        <Alert
                            severity="success"
                            variant="string"
                            sx={{
                                width: '60%',
                                m: 'auto',
                                visibility: showAlert ? 'visible' : 'hidden'
                            }}
                        >
                            Score Updated.
                        </Alert>
                    </CardContent>
                </Box>
                <CardMedia component="img" sx={{ width: 220 }} image="./result.png" />
            </Card>
            <Answer qnAnswers={qnAnswers} />
        </>
    );
}