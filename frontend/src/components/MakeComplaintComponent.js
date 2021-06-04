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
import headers from "../shared/authheader";
import apiURL from "../shared/apiURL"
import UserContext from "../contexts/UserContext";
import {Link} from 'react-router-dom';

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

const MakeComplaint = (props) => {
    const { setSnackbar } = useContext(SnackbarContext);
    const {user} = useContext(UserContext);
    const [content, setContent] = useState("");
    const [added, setAdded] = useState(false);
    var orderId = props.orderIdd;
    const classes = useStyles();
    const onContentChange = (event) =>{
        setContent(event.target.value);
    }
    
    const saveNewComplaint = async () => {
        var config = {
            method: 'post',
            url: apiURL + 'complaint',
            headers: headers(user),
            data: {
                "content": content,
                "orderId": props.orderIdd
              }
        };

        try
        {
            console.log(headers(user));
            await axios(config);
            setAdded(true);
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
    }

    return(
<div>
      { added ? 
        <div>
            <Typography style={{margin:150}} variant="h4">Added succesfully!</Typography>
            <Button variant="contained" color="default" size="large">
                <Link to="/OrdersHistory">
                    Back
                </Link>
            </Button>
        </div>
        :
        <React.Fragment>
          <CssBaseline/>
          <AppBar>
            <Toolbar>
              <Button>
                <Link to="/OrdersHistory">
                <ArrowBackIcon fontSize = "large"/>
                </Link>
              </Button>
              <Typography variant="h6" color="inherit" noWrap>
                Make a Complaint
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
                          Make a complaint for order: {props.orderId}
                        </Typography>
                        <br/>                        
                        <TextField
                            id="outlined-multiline-static"
                            label="Make a complaint"
                            multiline
                            defaultValue=""
                            variant="outlined"
                            fullWidth={true}
                            onChange = {onContentChange}
                        />
                        <br/>
                        <br/>
                        <Button variant="contained" color="primary" onClick={() => saveNewComplaint()}>
                          <Typography variant="button" color="inherit">
                            Save
                          </Typography>
                        </Button>
                      </CardContent>
                    </Card>
                  </Grid>
              </Grid>
            </Container>
          </div>
        </React.Fragment> 
      }
      </div>
    )
}

export default MakeComplaint;