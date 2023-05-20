import { Post } from "../Models/Post";

export const MOCK_POSTS: Post[] = [
    {
        id: 101,
        title: 'Comment faire des bébés ?',
        content: `
        # Comment on fait les bébés ?
        Pour faire un bébé, il faut une femme avec **un ovocyte**, une cellule fabriquée par son corps chaque mois, et un homme avec des millions de **spermatozoïdes**. Quand un couple fait l’amour, les spermatozoïdes microscopiques passent du corps de l’homme à celui de la femme pour rejoindre l’ovocyte. Un seul spermatozoïde peut entrer à l’intérieur de l’ovocyte pour le féconder. Mais le spermatozoïde n’atteint pas toujours son but. En effet, l’ovocyte est fécondable seulement 4 jours par mois.
        Quand la **fécondation** a lieu, cette union entre l’ovocyte et le spermatozoïde forme un œuf. Cet œuf s’installe dans l’utérus de la femme. L'utérus est une sorte de poche située dans le ventre de la femme. C’est là que l’œuf va se diviser en millions de cellules pour former un embryon, puis un fœtus, et enfin un bébé qui naîtra 9 mois plus tard.
        `,
        user_id: 0,
        ratings: [],
        postTags: [],
        created_At: new Date('2023-05-16 19:46:14'),
        updated_at: new Date('2023-05-16 19:46:14'),
        tags: '1, 2'
    },  
    {
        id: 102,
        title: 'On prank nos petits frères ! ça tourne mal',
        content: `
        # Prépare une attaque-surprise depuis son placard.
        Choisis d'abord un moment de la journée où ton frère pense que tu n'es pas à la maison. Ensuite, cache-toi dans un placard assez éloigné de là où il se trouve (comme celui de ta chambre par exemple), puis appelle le numéro de la maison depuis ton portable. Lorsque ton frère décroche, demande-lui d'aller chercher quelque chose dans ton placard (ou celui dans lequel tu te trouves). Lorsque la porte s'ouvre, bondis sur lui et crie « Bouuuh ! » Il sera alors très surpris de te voir et aura la peur de sa vie ! Tu pourras te moquer de lui pour le restant de ses jours. 
        `,
        user_id: 0,
        ratings: [],
        postTags: [],
        created_At: new Date('2023-05-16 19:46:14'),
        updated_at: new Date('2023-05-16 19:46:14'),
        tags: '1, 2'
    },
    {
        id: 103,
        title: 'Il saute du 1er étage pour sauver un lapin',
        content: `
        # Un homme brave le danger en sautant du premier étage pour sauver un lapin
        Dans une scène digne d'un film d'action, un homme courageux a risqué sa propre vie en sautant du premier étage d'un immeuble pour sauver un lapin en détresse. Cet acte héroïque a eu lieu dans la matinée alors que de nombreux témoins incrédules observaient la scène avec admiration.

        L'homme, dont l'identité reste inconnue pour le moment, a été alerté par les cris désespérés d'un voisin signalant qu'un lapin se trouvait piégé sur une corniche extérieure du premier étage. Sans hésitation, l'homme a immédiatement pris la décision de secourir l'animal en danger.

        Armé d'une détermination sans faille, l'homme a escaladé rapidement l'extérieur de l'immeuble, attirant l'attention de tous ceux qui se trouvaient à proximité. Alors que les pompiers et les secours étaient en route, le héros improvisé n'a pas attendu leur arrivée. Il a préféré prendre le risque et agir rapidement pour sauver le petit lapin vulnérable.
        `,
        user_id: 0,
        ratings: [],
        postTags: [],
        created_At: new Date('2023-05-16 19:46:14'),
        updated_at: new Date('2023-05-16 19:46:14'),
        tags: '1, 2'
    },
    
]