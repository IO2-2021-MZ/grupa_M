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

  const AllComplaints = (props) => {
    const classes = useStyles();

    const {restaurantId, restaurantName} = props;
    const { setLoading } = useContext(LoadingContext);
    const { setSnackbar } = useContext(SnackbarContext);
    const [complaints, setComplaints] = useState([]);

    const closeComplaint = async (id) => {
    }

    const deleteComplaint = async (id) => {
      setLoading(true);
      var config = {
        method: 'delete',
        url: "https://localhost:44384/complaint?id=" + id
    }
    
    try
    {
        const response = await axios(config);
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
    fetchData(restaurantId);
    }

    async function fetchData(id){          
      var config = {
          method: 'get',
          url: 'https://localhost:44384/restaurant/complaint/all?id=' + id
      }
      
      try
      {
          const response = await axios(config);
          setComplaints(response.data);
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
    }, [setComplaints, setLoading, setSnackbar, restaurantId]);

    return(
        complaints === undefined ? <div>Loading...</div> :
        
        <React.Fragment>
          <CssBaseline/>
          <AppBar>
            <Toolbar>
              <Button>
                <ArrowBackIcon fontSize = "large"/>
              </Button>
              <Typography variant="h6" color="inherit" noWrap>
                Complaints of {restaurantName}
              </Typography>
            </Toolbar>
          </AppBar>
          <div className={classes.heroContent}>
            <Container className = {classes.cardGrid} maxWidth="md">
              <Grid container spacing = {4} alignItems="center">
                {complaints.map((complaint) => (
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
                        {complaint.response === null ?
                        <Typography variant="button" color="inherit">
                        </Typography> :
                        <Typography variant="h6" color="textSecondary">
                          {complaint.response}
                        </Typography>
                        }
                        <br/>
                        <CardActions>
                          {complaint.open ?
                          <Button variant="contained" color="primary" onClick={() => closeComplaint(complaint.id)}>
                            <Typography variant="button" color="inherit">
                              Response
                            </Typography>
                          </Button> :
                          <Button variant="contained" color="primary" disabled={true}>
                            <Typography variant="button" color="inherit">
                              Closed
                            </Typography>
                          </Button>
                          }
                          <Button variant="contained" color="secondary" onClick={() => deleteComplaint(complaint.id)}>
                            <Typography variant="button" color="inherit">
                              Delete
                            </Typography>
                          </Button>
                        </CardActions>
                      </CardContent>
                    </Card>
                  </Grid>
                ))}
              </Grid>
            </Container>
          </div>
        </React.Fragment>
    )
}

export default AllComplaints;