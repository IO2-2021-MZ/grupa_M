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
import { makeStyles, withStyles } from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import Link from "@material-ui/core/Link";
import HomeIcon from "@material-ui/icons/Home";
import SnackbarContext from "../contexts/SnackbarContext";
import LoadingContext from "../contexts/LoadingContext";
import axios from "axios";
import { purple } from "@material-ui/core/colors";
import { Link as RouterLink } from "react-router-dom";
import TextField from "@material-ui/core/TextField";
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

const ColorButton = withStyles((theme) => ({
  root: {
    color: theme.palette.getContrastText(purple[500]),
    backgroundColor: purple[500],
    "&:hover": {
      backgroundColor: purple[700],
    },
  },
}))(Button);

export default function RestaurateurRestaurantList() {
  const classes = useStyles();
  const { setLoading } = useContext(LoadingContext);
  const { setSnackbar } = useContext(SnackbarContext);
  const [rests, setRests] = useState([]);

  async function fetchData() {
    setLoading(true);
    var config = {
      method: "get",
      url: "https://localhost:5001/restaurant/all", //apiUrl + 'restaurant/all',
      headers: {
        //'Authorization': 'Bearer '+ user.token
        Authorization:
          "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2MjE1OTM0MjcsImV4cCI6MTYyMTU5NDMyNywiaWF0IjoxNjIxNTkzNDI3fQ.WwVi1qZvhQUIVBrqby2JJQxiW7qzuU83Ac727zu8JoU",
      },
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

  return (
    <React.Fragment>
      <AppBar position="relative">
        <Toolbar>
          <HomeIcon className={classes.icon} />
          <Typography variant="h6" color="inherit" noWrap>
            restaurant List
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
          </Container>
        </div>
        <Container className={classes.cardGrid} maxWidth="md">
          {/* End hero unit */}
          <Grid container spacing={12}>
            {rests.map((rest) => (
              <Grid item key={rest.id} xs={12} sm={6} md={4}>
                <Card className={classes.card}>
                  <CardMedia
                    className={classes.cardMedia}
                    title="Image title"
                  />
                  <CardContent className={classes.cardContent}>
                    <Typography gutterBottom variant="h5" component="h2">
                      {rest.name}
                    </Typography>
                  </CardContent>
                  <CardActions>
                    <ColorButton
                      type="submit"
                      fullWidth
                      variant="contained"
                      color="primary"
                      className={classes.submit}
                    >
                      Show Details
                    </ColorButton>
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
