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
import Rating from '@material-ui/lab/Rating';
import Box from '@material-ui/core/Box';
import headers from "../shared/authheader";
import apiURL from "../shared/apiURL"
import UserContext from "../contexts/UserContext";

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

const AllReviews = (props) => {
    const classes = useStyles();
    const restaurantId = props.RestaurantId;
    const restaurantName = props.RestaurantName;
    const {user} = useContext(UserContext)
    const { setLoading } = useContext(LoadingContext);
    const { setSnackbar } = useContext(SnackbarContext);

    const [rating, setRating] = useState();

    const [reviews, setReviews] = useState([]);

    async function fetchData(id){          
        var config = {
            method: 'get',
            url: apiURL + "restaurant/review/all?id=" + id,
            headers: headers(user)
        }
        
        try
        {
            const response = await axios(config);
            setReviews(response.data);

            console.log(
                {response}
              );
        }
        catch(e)
        {
            console.error(e);
            setSnackbar({
                open: true,
                message: 'Error occured',
                type: 'error'
            })
        }
        setLoading(false);
    }

    useEffect(() => {  
        setLoading(true);    
        fetchData(restaurantId);
    }, [setReviews, setLoading, setSnackbar, restaurantId]);

    return(
        reviews === undefined ? <div>Loading...</div> :
        
        <React.Fragment>
          <CssBaseline/>
          <AppBar>
            <Toolbar>
              <Button>
                <ArrowBackIcon fontSize = "large"/>
              </Button>
              <Typography variant="h6" color="inherit" noWrap>
                Reviews of {restaurantName}
              </Typography>
            </Toolbar>
          </AppBar>
          <div className={classes.heroContent}>
            <Container className = {classes.cardGrid} maxWidth="md">
              <Grid container spacing = {4} alignItems="center">
                {reviews.map((review) => (
                  <Grid item key={review.id} xs={12} >
                    <Card className={classes.card}>
                      <CardMedia className = {classes.cardMedia}/>
                      <CardContent className={classes.cardContent}>
                        <Typography variant="h5" align="left" color="textPrimary">
                          {review.content}
                        </Typography>
                      </CardContent>
                      <Box component="fieldset" mb={3} borderColor="transparent">
                        <Typography component="legend">Rating</Typography>
                        <Rating name="read-only" value={review.rating} readOnly />
                    </Box>
                    </Card>
                  </Grid>
                ))}
              </Grid>
            </Container>
          </div>
        </React.Fragment>
    )
}


export default AllReviews;
