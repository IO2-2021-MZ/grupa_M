import { useState, useContext, useEffect } from "react";
import SnackbarContext from '../contexts/SnackbarContext';
import LoadingContext from '../contexts/LoadingContext';
import axios from 'axios';


export default function AllComplaints(props){
    
    const {restaurantId} = props;

    const { setLoading } = useContext(LoadingContext);
    const { setSnackbar } = useContext(SnackbarContext);
    const [complaints, setComplaints] = useState([]);

    useEffect(() =>{
        async function fetchData(){
            
            setLoading(true);

            var config = {
                method: 'get',
                url: 'some-url',
                headers:{
                    'security-header': 'some-token'
                }
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
                    messafe: 'Error occured',
                    type: 'error'
                })
            }
            setLoading(false);
        }

        fetchData();

    }, [setComplaints, setLoading, setSnackbar]);

    return(
        <div>
            Complaints
        </div>
    )
}