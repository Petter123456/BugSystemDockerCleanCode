describe('Base', function () {
    it('should load home page', function () {
    cy.visit('https://localhost:49163/bugs')

    })
    it('Page should contain crud operations', () => {
        cy.get('#bugsCrudOperations');
    })
    it('Should reload the page upon press of crud buttons', () => {
        cy.get("#bugName").type("no product")
        cy.get('#update').click();
        cy.url().should('eq', 'https://localhost:49163/bugs');
    })
    it('Should  reset the state of the webapp', () => {
        cy.visit('https://localhost:49163/bugs');
        cy.url().should('eq', 'https://localhost:49163/bugs');
    })
})