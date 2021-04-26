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
import TextField from '@material-ui/core/TextField';


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

  const ComplaintResponse = (props) => {
    const classes = useStyles();

    const { complaintId } = props;
    const { setLoading } = useContext(LoadingContext);
    const { setSnackbar } = useContext(SnackbarContext);
    const [complaint, setComplaint] = useState();
    const [content, setResponse] = useState("");

    const onTextChange = (event) =>{
        setResponse( event.target.value);
    }

    const saveResponse = async (id) => {
        var config = {
            method: 'post',
            url: 'https://localhost:44384/complaint/respond?id='+id,
            header:{
                'Content-Type': 'application/json'
            },
            data: content
        };

        try
        {
            console.log(content);
            await axios(config);
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

    async function fetchData(id){          
      var config = {
          method: 'get',
          url: 'https://localhost:44384/complaint?id='+id
      }
      
      try
      {
          const response = await axios(config);
          setComplaint(response.data);
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
        fetchData(complaintId);
    }, [setComplaint, setLoading, setSnackbar, complaintId, fetchData]);

    return(
        complaint === undefined ? <div>Loading...</div> :
        
        <React.Fragment>
          <CssBaseline/>
          <AppBar>
            <Toolbar>
              <Button>
                <ArrowBackIcon fontSize = "large"/>
              </Button>
              <Typography variant="h6" color="inherit" noWrap>
                Response
              </Typography>
            </Toolbar>
          </AppBar>
          <div className={classes.heroContent}>
            <Container className = {classes.cardGrid} maxWidth="md">
              <Grid container spacing = {4} alignItems="center">
                  <Grid item key={complaint.id} xs={12} >
                    <Card className={classes.card}>
                      <CardMedia
                        className = {classes.cardMedia}
                      />
                      <CardContent className={classes.cardContent}>
                        <Typography variant="h5" align="left" color="textPrimary">
                          {complaint.content}
                        </Typography>
                        <br/>                        
                        <TextField
                            id="outlined-multiline-static"
                            label="Response"
                            multiline
                            defaultValue=""
                            variant="outlined"
                            fullWidth={true}
                            onChange = {onTextChange}
                        />
                        <br/>
                        <br/>
                        <Button variant="contained" color="primary" onClick={() => saveResponse(complaintId)}>
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
    )
}

export default ComplaintResponse;