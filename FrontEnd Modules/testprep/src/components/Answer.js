import React, { useState } from 'react';
import { Accordion, AccordionDetails, AccordionSummary, CardMedia, List, ListItem, Typography } from '@mui/material';
import { Box } from '@mui/system';
import { BASE_URL } from '../api';
import ExpandCircleDownIcon from '@mui/icons-material/ExpandCircleDown';
import { red, green, grey } from '@mui/material/colors';

export default function Answer({ qnAnswers }) {
    const [expanded, setExpanded] = useState(false);

    const handleChange = (panel) => (event, isExpanded) => {
        setExpanded(isExpanded ? panel : false);
    };

    const getExpandIconColor = (qna) => {
        if (qna.answer === qna.selected) {
            return green[500]; // Correct answer
        } else if (qna.selected !== null) {
            return red[500]; // Incorrect answer
        } else {
            return grey[500]; // Skipped question
        }
    };

    return (
        <Box sx={{ mt: 5, width: '100%', maxWidth: 640, mx: 'auto' }}>
            {
                qnAnswers.map((item, j) => (
                    <Accordion
                        disableGutters
                        key={j}
                        expanded={expanded === j}
                        onChange={handleChange(j)}
                    >
                        <AccordionSummary expandIcon={<ExpandCircleDownIcon sx={{ color: getExpandIconColor(item) }} />}>
                            <Typography sx={{ width: '90%', flexShrink: 0 }}>
                                {item.qnInWords}
                            </Typography>
                        </AccordionSummary>
                        <AccordionDetails sx={{ backgroundColor: grey[900] }}>
                            {item.imageName && (
                                <CardMedia
                                    component="img"
                                    image={BASE_URL + 'Images/' + item.imageName}
                                    sx={{ m: '10px auto', width: 'auto' }}
                                />
                            )}
                            <List>
                                {item.options.map((x, i) => (
                                    <ListItem key={i}>
                                        <Typography sx={{ color: item.answer === i ? green[500] : (item.selected === i ? red[500] : grey[500]) }}>
                                            <b>{String.fromCharCode(65 + i) + ". "}</b>
                                            {x}
                                        </Typography>
                                    </ListItem>
                                ))}
                            </List>
                        </AccordionDetails>
                    </Accordion>
                ))
            }
        </Box>
    );
}
