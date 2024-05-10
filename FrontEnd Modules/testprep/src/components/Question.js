import React, { useState, useEffect } from 'react';
import { BASE_URL } from '../api';
import useStateContext from '../Hooks/UseStateContext';
import { Card, CardContent, CardMedia, CardHeader, List, ListItemButton, Typography, Box, LinearProgress, FormControl, FormGroup, FormControlLabel, Checkbox, Button, TextField, Container } from '@mui/material';
import { getFormatedTime } from '../helper';
import { useNavigate } from 'react-router';

export default function Quiz() {
    const [qns, setQns] = useState([]);
    const [qnIndex, setQnIndex] = useState(0);
    const [timeTaken, setTimeTaken] = useState(0);
    const { context, setContext } = useStateContext();
    const navigate = useNavigate();
    const [categories, setCategories] = useState([
        { id: '1', name: 'English', checked: false },
        { id: '2', name: 'IQ', checked: false },
        { id: '3', name: 'Basic Math', checked: false },
        { id: '4', name: 'Advance Math', checked: false },
        { id: '5', name: 'Bio', checked: false },
        { id: '6', name: 'Chemistry', checked: false },
        { id: '7', name: 'Physics', checked: false },
    ]);
    const [questionCount, setQuestionCount] = useState(5);
    const [formSubmitted, setFormSubmitted] = useState(false);

    let timer;

    const startTimer = () => {
        clearInterval(timer); // Clear any existing timer
        timer = setInterval(() => {
            setTimeTaken(prev => prev + 1);
        }, 1000);
    };

    const fetchQuestions = () => {
        const selectedCategories = categories.filter(cat => cat.checked).map(cat => cat.id);
        const isAllChecked = categories.some(cat => cat.name === 'All' && cat.checked);

        if (selectedCategories.length === 0 || isAllChecked) {
            fetch(`${BASE_URL}/api/Question/GetAll?count=${questionCount}`)
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    return response.json();
                })
                .then(data => {
                    if (data.length !== questionCount) {
                        console.error('The number of questions fetched does not match the expected count');
                    }
                    setQns(data);
                    startTimer();
                })
                .catch(error => {
                    console.error('There was a problem with your fetch operation:', error);
                });
        } else {
            const url = `${BASE_URL}/api/Question?CategoryIds=${selectedCategories.join('&CategoryIds=')}&Count=${questionCount}`;
            fetch(url)
                .then(response => {
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    return response.json();
                })
                .then(data => {
                    if (data.length !== questionCount) {
                        console.error('The number of questions fetched does not match the expected count');
                    }
                    setQns(data);
                    startTimer();
                })
                .catch(error => {
                    console.error('There was a problem with your fetch operation:', error);
                });
        }
    };

    useEffect(() => {
        if (formSubmitted && categories.length > 0) {
            fetchQuestions();
        }
    }, [formSubmitted, categories]);

    const handleFormSubmit = async (e) => {
        e.preventDefault();
        if (categories && categories.length > 0) {
            setFormSubmitted(true);
        } else {
            console.error('Categories state is undefined or empty.');
        }
    };

    const handleCategoryChange = (id) => {
        const updatedCategories = categories.map(cat =>
            cat.id === id ? { ...cat, checked: !cat.checked } : cat
        );
        setCategories(updatedCategories);
    };

    const handleQuestionCountChange = (e) => {
        setQuestionCount(e.target.value);
    };

    const updateAnswer = (qnId, optionIdx) => {
        const temp = [...context.selectedOptions];
        const index = temp.findIndex(item => item.qnId === qnId);
        if (index !== -1) {
            temp[index].selected = optionIdx;
        } else {
            temp.push({
                qnId,
                selected: optionIdx
            });
        }
    
        // Treat unanswered questions as incorrect when moving to the next question
        if (qnIndex < questionCount - 1) {
            if (temp[index]?.selected === null) {
                // If the question is unanswered, mark it as incorrect
                temp[index].selected = -1;
            }
            setContext({ selectedOptions: [...temp] });
            setQnIndex(qnIndex + 1);
        } else {
            // Add skipped questions to selectedOptions state
            for (let i = qnIndex + 1; i < questionCount; i++) {
                temp.push({
                    qnId: qns[i].qnId,
                    selected: -1 // Mark as unanswered
                });
            }
            setContext({ selectedOptions: [...temp], timeTaken });
            navigate("/result");
        }
    };
    
    const handleBack = () => {
        if (qnIndex > 0) {
            setQnIndex(qnIndex - 1);
        }
    };

    useEffect(() => {
        setQns([]);
        setQnIndex(0);
        setFormSubmitted(false);
        setTimeTaken(0);
        setContext({
            participantId: 0,
            timeTaken: 0,
            selectedOptions: []
        });
    }, []);

    return (
        <Container maxWidth="md">
            <Typography variant="h3" align="center" gutterBottom>
                Test Preparation
            </Typography>
            <Typography variant="body1" gutterBottom>
                Choose any category to prepare for your test.
            </Typography>
            {!formSubmitted ? (
                <form onSubmit={handleFormSubmit}>
                    <FormControl sx={{ m: 2 }}>
                        <FormGroup>
                            <FormControlLabel
                                control={<Checkbox
                                    checked={categories.every(cat => cat.checked)}
                                    onChange={() => {
                                        const allChecked = categories.every(cat => cat.checked);
                                        const updatedCategories = categories.map(cat => ({ ...cat, checked: !allChecked }));
                                        setCategories(updatedCategories);
                                    }}
                                />}
                                label="All"
                            />
                            {categories.map(cat => (
                                categories.length > 0 && (
                                    cat && (
                                        <FormControlLabel
                                            key={cat.id}
                                            control={<Checkbox
                                                checked={cat.checked || false}
                                                onChange={() => handleCategoryChange(cat.id)}
                                                disabled={categories.every(c => c && c.id !== cat.id && c.checked)}
                                            />}
                                            label={cat.name}
                                        />
                                    )
                                )
                            ))}
                        </FormGroup>
                    </FormControl>

                    <FormControl sx={{ m: 2 }}>
                        <TextField
                            type="number"
                            label="Number of Questions"
                            value={questionCount}
                            onChange={handleQuestionCountChange}
                            inputProps={{ min: 1, max: 100 }}
                        />
                    </FormControl>
                    <Button variant="contained" type="submit">Submit</Button>
                </form>
            ) : null}
            {qns.length !== 0 ? (
                <Card
                    sx={{
                        maxWidth: 640, mx: 'auto', mt: 5,
                        '& .MuiCardHeader-action': { m: 0, alignSelf: 'center' }
                    }}
                >
                    <CardHeader
                        title={'Question ' + (qnIndex + 1) + ' of ' + questionCount}
                        action={
                            <>
                                <Button variant="contained" onClick={handleBack} disabled={qnIndex === 0}>Back</Button>
                                <Typography>{getFormatedTime(timeTaken)}</Typography>
                            </>
                        }
                    />
                    <Box>
                        <LinearProgress variant="determinate" value={(qnIndex + 1) * 100 / questionCount} />
                    </Box>
                    {qns[qnIndex].imageName != null ? (
                        <CardMedia
                            component="img"
                            image={BASE_URL + 'Images/' + qns[qnIndex].imageName}
                            sx={{ width: 'auto', m: '10px auto' }}
                        />
                    ) : null}
                    <CardContent>
                        <Typography variant="h6">
                            {qns[qnIndex].qnInWords}
                        </Typography>
                        <List>
                            {qns[qnIndex].options && qns[qnIndex].options.map((item, idx) => (
                                <ListItemButton
                                    key={idx}
                                    disableRipple
                                    onClick={() => updateAnswer(qns[qnIndex].qnId, idx)}
                                    sx={{
                                        backgroundColor: context.selectedOptions.find(option => option.qnId === qns[qnIndex].qnId && option.selected === idx) ? '#3399FF' : 'transparent'
                                    }}
                                >
                                    <div>
                                        <b>{String.fromCharCode(65 + idx) + " . "}</b>{item}
                                    </div>
                                </ListItemButton>
                            ))}
                        </List>
                    </CardContent>
                </Card>
            ) : null}
        </Container>
    );
}
