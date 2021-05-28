function authHeader(user){
    return {
        'api-key': user.apiKey,
        'Accept': 'application/json, text/plain',
        'Content-Type': 'application/json;charset=UTF-8' 
    };
}

export default authHeader;