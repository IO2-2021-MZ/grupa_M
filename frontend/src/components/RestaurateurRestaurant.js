import React, { useState, useContext, useEffect } from "react";
import SnackbarContext from "../contexts/SnackbarContext";
import LoadingContext from "../contexts/LoadingContext";
import axios from "axios";
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
import HomeIcon from "@material-ui/icons/Home";
import Rating from "@material-ui/lab/Rating";
import StarBorderIcon from "@material-ui/icons/StarBorder";
import Box from "@material-ui/core/Box";
import { Link as RouterLink } from "react-router-dom";

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

export default function RestaurateurRestaurant(props) {
  const classes = useStyles();

  const { restId } = props;
  const { setLoading } = useContext(LoadingContext);
  const { setSnackbar } = useContext(SnackbarContext);
  const [restaurant, setRestaurant] = useState({
    owing: 0.0,
    aggregatePayment: 0.0,
    id: 0,
    name: "",
    contactInformation: "",
    rating: 0.0,
    state: "Active",
    address: {
      city: "",
      street: "",
      postCode: "",
    },
  });
  const { user, setUser } = useContext(UserContext);

  async function fetchData() {
    setLoading(true);
    console.log(props);
    console.log(restId);
    var config = {
      method: "get",
      url: apiUrl + "restaurant?id=" + restId,
      headers: {
        Authorization: "Bearer " + user.token,
      },
    };

    try {
      const response = await axios(config);
      console.log(response);
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
    fetchData();
  }, [setRestaurant]);

  return (
    <React.Fragment>
      <CssBaseline />
      <AppBar>
        <Toolbar>
          <Button>
            <RouterLink
              to={"/RestaurateurRestaurantList"}
              style={{ color: "#FFF" }}
            >
              <ArrowBackIcon fontSize="large" />
            </RouterLink>
          </Button>
          <Typography variant="h6" color="inherit" noWrap>
            Restaurant View
          </Typography>
        </Toolbar>
      </AppBar>
      <div className={classes.heroContent}>
        <Container className={classes.cardGrid} maxWidth="md">
          <Grid container spacing={4} alignItems="center">
            <Grid xs={12}>
              <Card>
                <CardContent>
                  <Typography variant="h4" color="textPrimary">
                    {restaurant.name}
                  </Typography>
                  <br />
                  <Typography gutterBottom variant="subtitle1" align="left">
                    Address:
                    {restaurant.address.postCode +
                      " " +
                      restaurant.address.city +
                      ", " +
                      restaurant.address.street}
                  </Typography>
                  <Typography gutterBottom variant="subtitle1" align="left">
                    Contact: {restaurant.contactInformation}
                  </Typography>
                  <Box
                    component="fieldset"
                    mb={3}
                    borderColor="transparent"
                    align="left"
                  >
                    <Rating
                      name={"customized-empty" + restaurant.id}
                      value={restaurant.rating}
                      precision={0.5}
                      emptyIcon={<StarBorderIcon fontSize="inherit" />}
                    />
                  </Box>
                  <Button variant="contained" color="primary">
                    <RouterLink
                      to={"/FinanceAndStats/" + restaurant.id}
                      style={{ color: "#FFF" }}
                    >
                      Show Finances
                    </RouterLink>
                  </Button>
                  &nbsp; &nbsp;
                  <Button variant="contained" color="primary">
                    <RouterLink
                      to={"/RestaurateurSections/" + restaurant.id}
                      style={{ color: "#FFF" }}
                    >
                      Show Sections
                    </RouterLink>
                  </Button>
                </CardContent>
              </Card>
            </Grid>
          </Grid>
        </Container>
      </div>
    </React.Fragment>
  );
}
