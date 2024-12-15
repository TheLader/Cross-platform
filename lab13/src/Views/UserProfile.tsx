import React from "react";
import { useAuth0 } from "@auth0/auth0-react";

const UserProfile: React.FC = () => {
    const { user, isLoading, error } = useAuth0();

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>Error: {error.message}</div>;
    }

    if (!user) {
        return <div>No user profile data found.</div>;
    }

    return (
        <div className="row">
            <div className="col-md-12">
                <div className="row">
                    <h2>User Profile</h2>

                    <div className="col-md-2">
                        <img src={user.picture} alt="Profile" className="img-rounded img-responsive" />
                    </div>
                    <div className="col-md-4">
                        <p>
                            <i className="glyphicon glyphicon-envelope"></i> EmailAddress: {user.email || "N/A"}
                        </p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default UserProfile;
