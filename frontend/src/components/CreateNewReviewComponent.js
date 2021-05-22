import { useState, useContext, useEffect } from "react";
import SnackbarContext from '../contexts/SnackbarContext';
import LoadingContext from '../contexts/LoadingContext';
import axios from 'axios';
import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import AppBar from '@material-ui/core/AppBar';
import { CardMedia, Container, CssBaseline } from "@material-ui/core";
import ArrowBackIcon from '@material-ui/icons/ArrowBack';
import Toolbar from '@material-ui/core/Toolbar';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Card from '@material-ui/core/Card';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import TextField from '@material-ui/core/TextField';
import Rating from '@material-ui/lab/Rating';
import Box from '@material-ui/core/Box';
import apiUrl from "../shared/apiURL"
import UserContext from "../contexts/UserContext"
import { Link as RouterLink } from 'react-router-dom';

const useStyles = makeStyles((theme) => ({
    icon: {
      marginRight: theme.spacing(2),
    },
    heroContent: {
      backgroundColor: theme.palette.background.paper,
      padding: theme.spacing(8, 0, 6),
    },
    heroButtons: {
      marginTop: theme.spacing(4),
    },
    cardGrid: {
      paddingTop: theme.spacing(8),
      paddingBottom: theme.spacing(8),
    },
    card: {
      display: 'flex',
      flexDirection: 'column',
      color: "default"
    },
    cardMedia: {
    
    },
    cardContent: {
      flexGrow: 1,
      color: "default"
    },
    footer: {
      backgroundColor: theme.palette.background.paper,
      padding: theme.spacing(6),
    },
  }));

const CreateReview = (props) => {
    const { setSnackbar } = useContext(SnackbarContext);
    const [content, setContent] = useState("");
    const [rating, setRating] = useState(1);
    const {user, setUser} = useContext(UserContext);
    const { setLoading } = useContext(LoadingContext);
    const [rest, setRest] = useState([]);
    const [added, setAdded] = useState(false);
    var restaurantId = props.restId;
    var customerId = user.id;
    const classes = useStyles();
    
    const onContentChange = (event) =>{
        setContent(event.target.value);
    }

    async function fetchData() {
      setLoading(true);
  
      var config = {
        method: 'get',
        url: apiUrl + "restaurant?id=" + restaurantId,
        headers: { 
          'Authorization': 'Bearer ' + user.token
        }
      };
      
      try
      {
        const response = await axios(config);
        setRest(response.data);
      }
      catch(error)
      {
        console.error(error);
                  setSnackbar({
                      open: true,
                      message: "Loading data failed",
                      type: "error"
                  });
      }
      setLoading(false);
      }
  
      useEffect(() => {
          fetchData();
      }, [setRest]);
 
    const saveNewReview = async () => {
      setLoading(true);
      var config = {
        method: 'post',
        url: apiUrl + "review",
        headers: { 
          'Authorization': 'Bearer ' + user.token
        },
        data:{
            "content": content,
            "rating": rating,
            "restaurantId": restaurantId,
            "customerId": customerId
        }
      };
      const response =  axios(config)
        .then(() => setLoading(false))
        .then(() => setAdded(true))
        .then(() => setLoading(false))
        .catch((error) => setSnackbar(error.message))
        .then(() => setLoading(false));
    }

    return(<div>
      { added ? 
            <div>
                <Typography style={{margin:150}} variant="h4">Added succesfully!</Typography>
                <Button variant="contained" color="default" size="large">
                    <RouterLink to="/RestaurantList">
                        Back
                    </RouterLink>
                </Button>
            </div>
            :

        <div>
        <React.Fragment>
          <CssBaseline/>
          <AppBar>
            <Toolbar>
              <Button>
                <ArrowBackIcon fontSize = "large"/>
              </Button>
              <Typography variant="h6" color="inherit" noWrap>
                Create New Review
              </Typography>
            </Toolbar>
          </AppBar>
          <div className={classes.heroContent}>
            <Container className = {classes.cardGrid} maxWidth="md">
              <Grid container spacing = {4} alignItems="center">
                  <Grid xs={12} >
                    <Card className={classes.card}>
                      <CardMedia
                        className = {classes.cardMedia}
                      />
                      <CardContent className={classes.cardContent}>
                        <Typography variant="h5" align="left" color="textPrimary">
                         {rest.name}
                        </Typography>
                        <Box component="fieldset" mb={3} borderColor="transparent">
                          <Typography component="legend">Rate restaurant</Typography>
                            <Rating
                            name="simple-controlled"
                            value={rating}
                            onChange={(event, newValue) => {
                            setRating(newValue);
                            }}
                            />
                        </Box>
                        <br/>                        
                        <TextField
                            id="outlined-multiline-static"
                            label="Write a review"
                            multiline
                            defaultValue=""
                            variant="outlined"
                            fullWidth={true}
                            onChange = {onContentChange}
                        />
                        <br/>
                        <br/>
                        <Button variant="contained" color="primary" onClick={() => saveNewReview()}>
                          <Typography variant="button" color="inherit">
                            Save {console.log(restaurantId)}
                          </Typography>
                        </Button>
                      </CardContent>
                    </Card>
                  </Grid>
              </Grid>
            </Container>
          </div>
        </React.Fragment> 
        </div>
        }
        </div>
    )
}

export default CreateReview;