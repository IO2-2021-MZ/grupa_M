import { useState, useContext, useEffect } from "react";
import SnackbarContext from "../contexts/SnackbarContext";
import LoadingContext from "../contexts/LoadingContext";
import axios from "axios";
import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Typography from "@material-ui/core/Typography";
import AppBar from "@material-ui/core/AppBar";
import { CardMedia, Container, CssBaseline } from "@material-ui/core";
import ArrowBackIcon from "@material-ui/icons/ArrowBack";
import Toolbar from "@material-ui/core/Toolbar";
import Button from "@material-ui/core/Button";
import Grid from "@material-ui/core/Grid";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardActions from "@material-ui/core/CardActions";
import TextField from "@material-ui/core/TextField";
import InputLabel from "@material-ui/core/InputLabel";
import MenuItem from "@material-ui/core/MenuItem";
import FormHelperText from "@material-ui/core/FormHelperText";
import FormControl from "@material-ui/core/FormControl";
import Select from "@material-ui/core/Select";
import apiUrl from "../shared/apiURL";
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
    display: "flex",
    flexDirection: "column",
    color: "default",
  },
  cardMedia: {},
  cardContent: {
    flexGrow: 1,
    color: "default",
  },
  footer: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(6),
  },
  formControl: {
    margin: theme.spacing(1),
    minWidth: 120,
  },
  selectEmpty: {
    marginTop: theme.spacing(2),
  },
}));

const FinanceAndStats = (props) => {
  const classes = useStyles();

  const { restaurantId } = props; //do zmiany
  const { setLoading } = useContext(LoadingContext);
  const { setSnackbar } = useContext(SnackbarContext);
  const [restaurant, setRestaurant] = useState([]);
  const { user, setUser } = useContext(UserContext);

  async function fetchData(id) {
    var config = {
      method: "get",
      // url: 'https://localhost:5001/restaurant?id='+1,
      // headers: {
      //     'Authorization': 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2MjE1MTgwOTIsImV4cCI6MTYyMTUxODk5MiwiaWF0IjoxNjIxNTE4MDkyfQ.MPO_yJ2V5eBGlnZW_KbLKcrgho7R85j1vmr82AHbN14'
      //   }
      url: apiUrl + "restaurant?id=" + restaurantId,
      headers: {
        Authorization: "Bearer " + user.token,
      },
    };

    try {
      const response = await axios(config);
      setRestaurant(response.data);
    } catch (e) {
      console.error(e);
      setSnackbar({
        open: true,
        message: "Error occured",
        type: "error",
      });
    }
    setLoading(false);
  }

  useEffect(() => {
    setLoading(true);
    fetchData(restaurantId);
  }, [setRestaurant]);

  return (
    <React.Fragment>
      <CssBaseline />
      <AppBar>
        <Toolbar>
          <Button>
            <ArrowBackIcon fontSize="large" />
          </Button>
          <Typography variant="h6" color="inherit" noWrap>
            Make New Order
          </Typography>
        </Toolbar>
      </AppBar>
      <div className={classes.heroContent}>
        <Container className={classes.cardGrid} maxWidth="md">
          <Grid container spacing={4} alignItems="center">
            <Grid xs={12}>
              <Card>
                <CardContent>
                  <Typography variant="h4" align="left" color="textPrimary">
                    Finances and statistics of restaurant "{restaurant.name}"
                  </Typography>
                  <br />
                  <Typography variant="h5" component="h2">
                    Owing: {restaurant.owing}
                  </Typography>
                  <Typography variant="h5" component="h2">
                    Aggregate Payment: {restaurant.aggregatePayment}
                  </Typography>
                </CardContent>
              </Card>
            </Grid>
          </Grid>
        </Container>
      </div>
    </React.Fragment>
  );
};

export default FinanceAndStats;
