function authHeader(user){
    return {
        headers: { 
            apiKey: user.apiKey,
            'Accept': 'application/json, text/plain',
            'Content-Type': 'application/json;charset=UTF-8' 
        }
    };
}

export default authHeader;