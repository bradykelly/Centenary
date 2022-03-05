import './App.css';
import {useAuth, useLoginWithRedirect} from "@frontegg/react";

function App() {
    const {user, isAuthenticated} = useAuth();
    const loginWithRedirect = useLoginWithRedirect();

    return (
        <div className="App">
            {isAuthenticated ? (
                <div>
                    <div>
                        <img src={user?.profilePictureUrl} alt={user?.name}/>
                    </div>
                    <div>
                        <span>Logged in as: {user?.name}</span>
                    </div>
                    <div>
                        <button onClick={() => alert(user.accessToken)}>Get access token</button>
                    </div>
                </div>
            ) : (
                <div>
                    <button onClick={() => loginWithRedirect()}>Login with Redirect</button>
                </div>
            )}
        </div>
    );
}

export default App;
