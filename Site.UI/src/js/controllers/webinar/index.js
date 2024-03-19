angular.module('app.controllers')
    .controller('webinarController', ['$scope', '$state', ($scope, $state) => {

        $scope.videos = [
            { title: 'Presented by Jennifer Harrison, BScPhm, MSc - Oct 27, 12:47 PM', src: '/video/medical-errors-en.mp4' }
        ];
        $scope.speakers = [
            {
                img: 'images/cathy-burger-min.jpg',
                memo: 'Speaker:',
                name: 'Cathy Burger, BScPhm, ACPR, RPh',
                title: 'Pharmacotherapy Specialist,',
                subTitle: 'Renal Transplant and Residency Coordinator',
                address: 'St. Joseph\'s Healthcare, Hamilton, ON',
                info: 'Cathy Burger graduated from the University of Toronto and went on to complete a Residency in Hospital Pharmacy at Mt. Sinai Hospital in Toronto.  She started her career at St. Pauls’ Hospital in Vancouver before returning to Mt. Sinai.  She then moved on to St. Joseph’s Healthcare Hamilton where she has been caring for renal transplant recipients for over 16 years, first on the inpatient unit and now in the outpatient clinic.'
            }
        ];

        if ($state.params.locale === 'fr') {

            $scope.videos = [
                { title: 'Présenté par Élisabeth Gélinas-Lemay, BPharm, MSc', src: '/video/medical-errors-fr.mp4' }
            ];

            $scope.speakers = [
                {
                    img: 'images/marie-josee-deschenes-min.jpeg',
                    memo: 'Conférencière:',
                    name: 'Marie-Josée Deschênes, B.Pharm, M.Sc.',
                    title: 'Pharmacienne clinicienne spécialisée,',
                    subTitle: 'Néphrologie / Transplantation',
                    address: 'L’Hôpital d’Ottawa, Ottawa, ON',
                    info: 'Marie-Josée Deschênes, graduée de l\'université de Montréal, est pharmacienne spécialisée en transplantation rénale du Programme de transplantation rénale de l\'Hôpital d\'Ottawa.Elle travaille au Programme de transplantation rénale de l\'Hôpital d\'Ottawa en tant que pharmacienne bilingue depuis plus de vingt ans, apportant son soutien aux patients transplantés rénaux, qu\'ils soient hospitalisés ou ambulatoires.'
                }
            ];
        }
    }]);