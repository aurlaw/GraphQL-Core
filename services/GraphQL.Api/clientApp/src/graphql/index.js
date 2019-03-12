import gql from "graphql-tag";

const PlayersFeedFfragments = {
    player: gql`
      fragment PlayerStats on PlayerType {
        id
          name
          height
          weightLbs
          skaterSeasonStats(limit: 5 sort: true) {
              id
              points
              season
            }          
      }
    `,
  };


export const leaguesQuery = gql`
    {
        leagues {
            id
            name
        } 
        notification  @client {
            message
            created
        }        
    } 
    
`;

export const teamsQuery = gql`
    {
        teams {
            id
            name
        } 
    } 
`;

export const seasonsQuery = gql`
    {
        seasons {
            id
            name
        } 
    } 
`;

export const playersQuery = gql`
      {
        players {
            ...PlayerStats
        }
      }    
      ${PlayersFeedFfragments.player}
`;


export const createPlayerMutation = gql`
    mutation ($player: PlayerInput!, $skaterStats: [SkaterStatisticInput]) {
        createPlayer(player: $player, skaterStats: $skaterStats) {
            ...PlayerStats
        }
    }
    ${PlayersFeedFfragments.player}
`;

export const deletePlayerMutation = gql`
    mutation ($playerId: Int!) {
        deletePlayer(playerId: $playerId) {
            id, statusType
        }
    }
`;