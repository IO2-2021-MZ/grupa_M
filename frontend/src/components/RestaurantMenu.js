import React, { useState, useEffect, useContext } from 'react';
import AppBar from '@material-ui/core/AppBar';
import Button from '@material-ui/core/Button';
import Card from '@material-ui/core/Card';
import CardActions from '@material-ui/core/CardActions';
import CardContent from '@material-ui/core/CardContent';
import CardMedia from '@material-ui/core/CardMedia';
import CssBaseline from '@material-ui/core/CssBaseline';
import Grid from '@material-ui/core/Grid';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import HomeIcon from '@material-ui/icons/Home';
import SnackbarContext from '../contexts/SnackbarContext';
import LoadingContext from '../contexts/LoadingContext';
import axios from 'axios';
import TextField from '@material-ui/core/TextField';
import apiUrl from "../shared/apiURL"
import UserContext from "../contexts/UserContext"
import Rating from '@material-ui/lab/Rating';
import StarBorderIcon from '@material-ui/icons/StarBorder';
import Box from '@material-ui/core/Box';
import { Link as RouterLink } from 'react-router-dom';
import headers from "../shared/authheader";


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
    height: '100%',
    display: 'flex',
    flexDirection: 'column',
  },
  cardMedia: {
    paddingTop: '56.25%', // 16:9
  },
  cardContent: {
    flexGrow: 1,
  },
  footer: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(6),
  },
}));

function RestaurantMenu(props) {
    const classes = useStyles();
    const { setLoading } = useContext(LoadingContext);
    const { setSnackbar } = useContext(SnackbarContext);
    const [sections, setSections] = useState([]);
    const {user, setUser} = useContext(UserContext);
    const {restId} = props;

    async function fetchData() {
        setLoading(true);
    
        var config = {
          method: 'get',
          url: apiUrl + "restaurant/menu?id=" + restId,
          headers: headers(user)
        };
        
        try
        {
          const response = await axios(config);
          setSections(response.data);
          
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
        }, [setSections]);

    return (
            <React.Fragment>
                <AppBar position="relative">
        <Toolbar>
          <HomeIcon className={classes.icon} />
          <Typography variant="h6" color="inherit" noWrap>
            Menu
          </Typography>
        </Toolbar>
      </AppBar>
      <main>
        {/* Hero unit */}
        <div className={classes.heroContent}>
          <Container maxWidth="sm">
            <Typography component="h1" variant="h2" align="center" color="textPrimary" gutterBottom>
              Menu
            </Typography>
          </Container>
        </div>
        <Container className={classes.cardGrid}>
          {/* End hero unit */}
            {sections.map((section) => (
                <Card className={classes.card}>
                  <CardContent className={classes.cardContent}>
                  <Typography gutterBottom variant="h5" component="h2">
                    {section.name}
                  </Typography>
                  {section.positions.map((position) => (
                    <Container maxWidth="sm" style={{padding:10}}>
                      <Typography variant="subtitle1" color="textPrimary">
                        {position.name}
                      </Typography>
                      <Typography variant="subtitle2" color="textPrimary" paragraph>
                        Price: {position.price}pln
                      </Typography>
                      <Typography variant="subtitle2" color="textSecondary" paragraph>
                        {position.description}
                      </Typography>
                      <Container align="right">
                        <Typography variant="subtitle2" color="textPrimary" paragraph>
                          Count: 0
                        </Typography>
                        <Button variant="contained" >-</Button>
                        <Button variant="contained" >+</Button>
                      </Container>
                    </Container>
                  ))}
                  </CardContent>
                </Card>
            ))}
        </Container>
      </main>
    </React.Fragment>
    )
}

export default RestaurantMenu
