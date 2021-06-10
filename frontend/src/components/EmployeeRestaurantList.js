import React, { useState, useEffect, useContext } from "react";
import AppBar from "@material-ui/core/AppBar";
import Button from "@material-ui/core/Button";
import Card from "@material-ui/core/Card";
import CardActions from "@material-ui/core/CardActions";
import CardContent from "@material-ui/core/CardContent";
import CardMedia from "@material-ui/core/CardMedia";
import CssBaseline from "@material-ui/core/CssBaseline";
import Grid from "@material-ui/core/Grid";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import { makeStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import HomeIcon from "@material-ui/icons/Home";
import SnackbarContext from "../contexts/SnackbarContext";
import LoadingContext from "../contexts/LoadingContext";
import axios from "axios";
import TextField from "@material-ui/core/TextField";
import apiUrl from "../shared/apiURL";
import UserContext from "../contexts/UserContext";
import Rating from "@material-ui/lab/Rating";
import StarBorderIcon from "@material-ui/icons/StarBorder";
import Box from "@material-ui/core/Box";
import { Link as RouterLink } from "react-router-dom";
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
    height: "100%",
    display: "flex",
    flexDirection: "column",
  },
  cardMedia: {
    paddingTop: "56.25%", // 16:9
  },
  cardContent: {
    flexGrow: 1,
  },
  footer: {
    backgroundColor: theme.palette.background.paper,
    padding: theme.spacing(6),
  },
}));

export default function RestaurateurRestaurantList() {
  const classes = useStyles();
  const { setLoading } = useContext(LoadingContext);
  const { setSnackbar } = useContext(SnackbarContext);
  const [rests, setRests] = useState([]);
  const { user, setUser } = useContext(UserContext);

  async function fetchData() {
    setLoading(true);

    var config = {
      method: "get",
      url: apiUrl + "restaurant/all",
      headers: headers(user)
    };

    try {
      const response = await axios(config);
      setRests(response.data);
    } catch (error) {
      console.error(error);
      setSnackbar({
        open: true,
        message: "Loading data failed",
        type: "error",
      });
    }
    setLoading(false);
  }

  useEffect(() => {
    fetchData();
  }, [setRests]);

  const deleteRestaurant = (id) => {
    setLoading(true);

    var config = {
      method: "delete",
      url: apiUrl + "restaurant?id=" + id,
      headers: headers(user)
    };

    const response = axios(config)
      .then(() => fetchData())
      .catch((error) => setSnackbar(error.message));
  };


  return (
    <React.Fragment>
      <AppBar position="relative">
        <Toolbar>
          <HomeIcon className={classes.icon} />
          <Typography variant="h6" color="inherit" noWrap>
            Restaurant List
          </Typography>
        </Toolbar>
      </AppBar>
      <main>
        {/* Hero unit */}
        <div className={classes.heroContent}>
          <Container maxWidth="sm">
            <Typography
              component="h1"
              variant="h2"
              align="center"
              color="textPrimary"
              gutterBottom
            >
              Restaurant List
            </Typography>
            <Typography
              variant="h5"
              align="center"
              color="textSecondary"
              paragraph
            >
              App allows to manage all restaurants.
            </Typography>
            <Button variant="contained" color="default">
                <RouterLink to="/RabatCodeList">
                  Discount codes
                </RouterLink>
              </Button>
              &nbsp; &nbsp;
          </Container>
        </div>
        <Container className={classes.cardGrid} maxWidth="md">
          {/* End hero unit */}
          <Grid container spacing={10}>
            {rests.map((rest) => (
              <Grid
                item
                key={rest.id}
                xs={12}
                sm={12}
                md={12}
                style={{ padding: 10 }}
              >
                <Card className={classes.card}>
                  <CardContent className={classes.cardContent}>
                    <Typography gutterBottom variant="h5" component="h2">
                      {rest.name}
                    </Typography>
                    <Typography gutterBottom variant="subtitle1">
                      {rest.address.postCode +
                        " " +
                        rest.address.city +
                        ", " +
                        rest.address.street}
                    </Typography>
                    <Typography gutterBottom variant="subtitle1">
                      {rest.contactInformation}
                    </Typography>
                    <Box component="fieldset" mb={3} borderColor="transparent">
                      <Rating
                        name={"customized-empty" + rest.id}
                        value={rest.rating}
                        precision={0.5}
                        readOnly
                        emptyIcon={<StarBorderIcon fontSize="inherit" />}
                      />
                    </Box>
                  </CardContent>
                  <CardActions>
                    <Button
                      variant="contained"
                      size="small"
                      color="primary"
                      style={{ margin: 15 }}
                      disabled={rest.state=="blocked" || rest.state=="disabled"}
                    >
                      <RouterLink
                        to={"/Restaurant/" + rest.id}
                        style={{ color: "#FFF" }}
                      >
                        Details
                      </RouterLink>
                    </Button>
                  </CardActions>
                </Card>
              </Grid>
            ))}
          </Grid>
        </Container>
      </main>
    </React.Fragment>
  );
}
